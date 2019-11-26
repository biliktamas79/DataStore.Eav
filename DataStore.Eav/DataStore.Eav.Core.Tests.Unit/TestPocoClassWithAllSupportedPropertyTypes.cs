using System;
using System.Collections.Generic;

namespace DataStore.Eav.Core.Tests.Unit
{
    public class TestPocoClassWithAllSupportedPropertyTypes
    {
        public readonly bool PublicReadonlyBoolField;
        public bool PublicBoolField;
        private readonly bool PrivateReadonlyBoolField;
        private bool PrivateBoolField;

        public readonly IEnumerable<bool> PublicReadonlyBoolEnumerableField;
        public IEnumerable<bool> PublicBoolEnumerableField;
        private readonly IEnumerable<bool> PrivateReadonlyBoolEnumerableField;
        private IEnumerable<bool> PrivateBoolEnumerableField;

        public readonly bool[] PublicReadonlyBoolArrayField;
        public bool[] PublicBoolArrayField;
        private readonly bool[] PrivateReadonlyBoolArrayField;
        private bool[] PrivateBoolArrayField;

        public readonly List<bool> PublicReadonlyBoolListField;
        public List<bool> PublicBoolListField;
        private readonly List<bool> PrivateReadonlyBoolListField;
        private List<bool> PrivateBoolListField;

        public readonly char PublicReadonlyCharField;
        public char PublicCharField;
        private readonly char PrivateReadonlyCharField;
        private char PrivateCharField;

        public readonly string PublicReadonlyStringField;
        public string PublicStringField;
        private readonly string PrivateReadonlyStringField;
        private string PrivateStringField;

        public readonly byte PublicReadonlyByteField;
        public byte PublicByteField;
        private readonly byte PrivateReadonlyByteField;
        private byte PrivateByteField;
    }
}
