using System;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace CoachTwinsApi.Db
{
    public class EntityEncryptor
    {
        private readonly IDataProtector _protector;

        public EntityEncryptor(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("entity_data");
        }

        public string Encrypt<T>(T value)
        {
            if (value == null)
                return null;
            
            string val;
            if (value is string s)
                val = s;
            else
                val = Convert.ToString(value);
            
            return _protector.Protect(val ?? "");
        }
        
        public T Decrypt<T>(string value)
        {
            if (value == null)
                return default;
            
            byte[] val = Base64UrlEncoder.DecodeBytes(value);
            string returnValue;

            try
            {
                returnValue = Encoding.UTF8.GetString(_protector.Unprotect(val));
            }
            catch (Exception e)
            {
                // ignore revoked keys errors, data is comming from a trusted source
                var decoded = (_protector as IPersistedDataProtector)?.DangerousUnprotect(val, true, out _, out _);

                returnValue = Encoding.UTF8.GetString(decoded);
            }

            return (T)Convert.ChangeType(returnValue, typeof(T));
        }
    }
}