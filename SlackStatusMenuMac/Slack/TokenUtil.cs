using Security;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    public static class TokenUtil
    {
        private const string KEYCHAIN_ACCOUNT = "tokens";
        private const string KEYCHAIN_SERVICE = "Slack Status Menu";

        public static List<string> LoadTokens()
        {
            if (ExistsSetting() == false)
            {
                CreateEmptySetting();
            }

            var query = new SecRecord(SecKind.GenericPassword)
            {
                Account = KEYCHAIN_ACCOUNT,
                Service = KEYCHAIN_SERVICE,
            };

            SecStatusCode status;

            var record = SecKeyChain.QueryAsRecord(query, out status);

            if (status == SecStatusCode.Success)
            {
                var data = record.ValueData;

                var tokens = JsonConvert.DeserializeObject<List<string>>(data.ToString());

                return tokens;
            }
            else
            {
                throw new Exception(status.ToString());
            }
        }

        public static void SaveTokens(List<string> tokens)
        {
            var record = new SecRecord(SecKind.GenericPassword)
            {
                Account = KEYCHAIN_ACCOUNT,
                Service = KEYCHAIN_SERVICE,
                ValueData = JsonConvert.SerializeObject(tokens),
            };

            var status = SecKeyChain.Update(record, record);

            if (status != SecStatusCode.Success) {
                throw new ApplicationException(status.ToString());
            }
        }

        private static bool ExistsSetting()
        {
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Account = KEYCHAIN_ACCOUNT,
                Service = KEYCHAIN_SERVICE,
            };

            SecStatusCode status;

            var record = SecKeyChain.QueryAsRecord(query, out status);

            if (status == SecStatusCode.Success) {
                return true;
            }

            if (status == SecStatusCode.ItemNotFound)
            {
                return false;
            }

            throw new ApplicationException(status.ToString());
        }

        private static void CreateEmptySetting()
        {
            var record = new SecRecord(SecKind.GenericPassword)
            {
                Account = KEYCHAIN_ACCOUNT,
                Service = KEYCHAIN_SERVICE,
                ValueData = "[]"
            };

            var status = SecKeyChain.Add(record);

            if (status != SecStatusCode.Success)
            {
                throw new Exception(status.ToString());
            }
        }

    }
}
