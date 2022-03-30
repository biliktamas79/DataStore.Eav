using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core
{
    public class ReflectionEntityDefinitionProvider : IEntityDefinitionProvider
    {
        /// <summary>
        /// Static cached instance of the reflected type info of IEnumerable
        /// </summary>
        private static readonly TypeInfo iEnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        /// <summary>
        /// Static cached instance of the reflected type info of IEnumerable<>
        /// </summary>
        private static readonly TypeInfo genericIEnumerableTypeInfo = typeof(IEnumerable<>).GetTypeInfo();

        private readonly IEntityDefinitionKeyProvider keyProvider;

        public ReflectionEntityDefinitionProvider(IEntityDefinitionKeyProvider keyProvider)
        {
            this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
        }

        /// <summary>
        /// Gets the entity definition for the given type, built by using reflection.
        /// </summary>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the entity definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        public Task<EntityDefinition> GetEntityDefinition(Type entityType, CancellationToken cancellationToken)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            var key = this.keyProvider.GetEntityDefinitionKey(entityType);

            cancellationToken.ThrowIfCancellationRequested();

            var entityDef = new EntityDefinition(key);

            foreach (var field in entityType.GetRuntimeFields()
                // only public, non-static, non-readonly fields that aren't backing fields for auto properties
                .Where(f => f.IsPublic && !f.IsStatic && !f.IsInitOnly && !f.GetCustomAttributes<CompilerGeneratedAttribute>().Any()))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var ad = new AttributeDefinition(field.Name, GetAttributeType(field.FieldType), GetAttributeFlags(field.FieldType));
                entityDef.Attributes.Add(ad);
            }

            foreach (var prop in entityType.GetRuntimeProperties()
                // only public, non-static, gettable properties
                .Where(p => IsPublic(p) && !IsStatic(p) && p.CanRead))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var ad = new AttributeDefinition(prop.Name, GetAttributeType(prop.PropertyType), GetAttributeFlags(prop.PropertyType));
                entityDef.Attributes.Add(ad);
            }

            return Task.FromResult(entityDef);
        }

        private static AttributeTypeEnum GetAttributeType(Type memberType)
        {
            if (memberType.IsArray)
                return GetAttributeType(memberType.GetElementType());

            if (memberType == typeof(bool))
                return AttributeTypeEnum.Boolean;
            else if (memberType == typeof(byte))
                return AttributeTypeEnum.Byte;
            else if (memberType == typeof(char))
                return AttributeTypeEnum.Char;
            else if (memberType == typeof(decimal))
                return AttributeTypeEnum.Decimal;
            else if (memberType == typeof(double))
                return AttributeTypeEnum.Double;
            else if (memberType == typeof(float))
                return AttributeTypeEnum.Float;
            else if (memberType == typeof(Guid))
                return AttributeTypeEnum.Guid;
            else if (memberType == typeof(Int16))
                return AttributeTypeEnum.Int16;
            else if (memberType == typeof(Int32))
                return AttributeTypeEnum.Int32;
            else if (memberType == typeof(Int64))
                return AttributeTypeEnum.Int64;
            else if (memberType == typeof(sbyte))
                return AttributeTypeEnum.SByte;
            else if (memberType == typeof(string))
                return AttributeTypeEnum.String;
            else if (memberType == typeof(UInt16))
                return AttributeTypeEnum.UInt16;
            else if (memberType == typeof(UInt32))
                return AttributeTypeEnum.UInt32;
            else if (memberType == typeof(UInt64))
                return AttributeTypeEnum.UInt64;

            var typeInfo = memberType.GetTypeInfo();

            // if it's a type that implements IEnumerable<T>
            if (typeInfo == genericIEnumerableTypeInfo || genericIEnumerableTypeInfo.IsAssignableFrom(typeInfo))
                return GetAttributeType(typeInfo.GenericTypeParameters[0]);
            //.inter(typeof(IEnumerable<>)))

            if (IsIEnumerableOfT(memberType) || iEnumerableTypeInfo.IsAssignableFrom(typeInfo))
                return GetAttributeType(typeInfo.GenericTypeArguments[0]);

            throw new NotSupportedException($"Type '{memberType}' is not supported!");
        }

        private static AttributeFlagsEnum GetAttributeFlags(Type memberType)
        {
            var flags = AttributeFlagsEnum.None;
            var typeInfo = memberType.GetTypeInfo();

            if ((memberType.IsByRef || memberType.IsClass || (Nullable.GetUnderlyingType(memberType) != null))
                // not having [Required] attribute
                && (typeInfo.GetCustomAttributes<RequiredAttribute>() == null))
                flags |= AttributeFlagsEnum.Nullable;

            // if it's string, then it is not a collection. (We have to handle it specially here as string implements IEnumerable<char> but don't want to handle string as a collection of characters.)
            if (memberType == typeof(string))
                return flags;

            if (IsOrderedCollectionType(typeInfo))
                flags |= AttributeFlagsEnum.Ordered | AttributeFlagsEnum.Collection;
            else if (IsEnumerableType(typeInfo) || IsCollectionType(typeInfo))
                flags |= AttributeFlagsEnum.Collection;

            return flags;
        }

        private static bool IsEnumerableType(TypeInfo ti)
        {
            return iEnumerableTypeInfo.IsAssignableFrom(ti)
                || genericIEnumerableTypeInfo.IsAssignableFrom(ti);
        }

        private static bool IsIEnumerableOfT(Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType
                   && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        private static bool IsCollectionType(TypeInfo ti)
        {
            return typeof(ICollection).GetTypeInfo().IsAssignableFrom(ti);
        }

        private static bool IsOrderedCollectionType(TypeInfo ti)
        {
            return ti.IsArray
                || typeof(IList).GetTypeInfo().IsAssignableFrom(ti)
                || typeof(IOrderedEnumerable<>).GetTypeInfo().IsAssignableFrom(ti);
        }

        private static bool IsStatic(PropertyInfo pi)
        {
            return (pi.CanRead ? pi.GetMethod : pi.SetMethod).IsStatic;
        }

        private static bool IsPublic(PropertyInfo pi)
        {
            return pi.CanRead
                ? pi.GetMethod.IsPublic
                : pi.CanWrite
                    ? pi.SetMethod.IsPublic
                    : false;
        }
    }
}
