using System;
using System.IO;

namespace CameraManager
{
    /// <summary>
    /// Provides access to messaging credentials that are stored outside of source control.
    /// </summary>
    public static class MessageSecretProvider
    {
        private const string SecretsRelativePath = "Config Setting/MessageSecrets.ini";
        private static readonly object SyncRoot = new object();
        private static MessageSecrets? _cachedSecrets;

        public static MessageSecrets GetSecrets()
        {
            lock (SyncRoot)
            {
                _cachedSecrets ??= LoadSecrets();
                return _cachedSecrets;
            }
        }

        public static void Reload()
        {
            lock (SyncRoot)
            {
                _cachedSecrets = LoadSecrets();
            }
        }

        private static MessageSecrets LoadSecrets()
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string secretsPath = Path.Combine(baseDirectory, SecretsRelativePath);
            string? secretsDirectory = Path.GetDirectoryName(secretsPath);

            if (!string.IsNullOrWhiteSpace(secretsDirectory) && !Directory.Exists(secretsDirectory))
            {
                Directory.CreateDirectory(secretsDirectory);
            }

            if (!File.Exists(secretsPath))
            {
                File.WriteAllLines(secretsPath, new[]
                {
                    "[TELEGRAM BOT TOKEN] ",
                    "[DISCORD BOT TOKEN] ",
                    "[DISCORD CHANNEL ID] ",
                    "[ESMS API KEY] ",
                    "[ESMS SECRET KEY] ",
                    "[ESMS OAID] ",
                    "[ESMS TEMPLATE ID] ",
                    "[ESMS BRANDNAME] ",
                    "[ESMS CALLBACK URL] https://esms.vn/webhook/",
                    "[ESMS CAMPAIGN ID] FireSmokeAlert"
                });
            }

            var secrets = new MessageSecrets
            {
                TelegramBotToken = ClassCommon.GetConfig(secretsPath, "TELEGRAM BOT TOKEN", string.Empty).Trim(),
                DiscordBotToken = ClassCommon.GetConfig(secretsPath, "DISCORD BOT TOKEN", string.Empty).Trim(),
                EsmsApiKey = ClassCommon.GetConfig(secretsPath, "ESMS API KEY", string.Empty).Trim(),
                EsmsSecretKey = ClassCommon.GetConfig(secretsPath, "ESMS SECRET KEY", string.Empty).Trim(),
                EsmsOaid = ClassCommon.GetConfig(secretsPath, "ESMS OAID", string.Empty).Trim(),
                EsmsTemplateId = ClassCommon.GetConfig(secretsPath, "ESMS TEMPLATE ID", string.Empty).Trim(),
                EsmsBrandName = ClassCommon.GetConfig(secretsPath, "ESMS BRANDNAME", string.Empty).Trim(),
                EsmsCallbackUrl = ClassCommon.GetConfig(secretsPath, "ESMS CALLBACK URL", "https://esms.vn/webhook/").Trim(),
                EsmsCampaignId = ClassCommon.GetConfig(secretsPath, "ESMS CAMPAIGN ID", "FireSmokeAlert").Trim()
            };

            string rawChannelId = ClassCommon.GetConfig(secretsPath, "DISCORD CHANNEL ID", "0").Trim();
            if (ulong.TryParse(rawChannelId, out ulong channelId))
            {
                secrets.DiscordChannelId = channelId;
            }

            return secrets;
        }
    }

    public sealed class MessageSecrets
    {
        public string TelegramBotToken { get; set; } = string.Empty;
        public string DiscordBotToken { get; set; } = string.Empty;
        public ulong DiscordChannelId { get; set; }
        public string EsmsApiKey { get; set; } = string.Empty;
        public string EsmsSecretKey { get; set; } = string.Empty;
        public string EsmsOaid { get; set; } = string.Empty;
        public string EsmsTemplateId { get; set; } = string.Empty;
        public string EsmsBrandName { get; set; } = string.Empty;
        public string EsmsCallbackUrl { get; set; } = string.Empty;
        public string EsmsCampaignId { get; set; } = string.Empty;
    }
}
