using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoachTwinsApi.Db.Converters
{
    public class EncryptionConverterFactory
    {
        private readonly EntityEncryptor _encryptor;

        public EncryptionConverterFactory(EntityEncryptor encryptor)
        {
            _encryptor = encryptor;
        }
        
        public ValueConverter Create<T>()
        {
            return new ValueConverter<T, string>(
                e => _encryptor.Encrypt(e),
                e => _encryptor.Decrypt<T>(e)
            );
        }
    }
}