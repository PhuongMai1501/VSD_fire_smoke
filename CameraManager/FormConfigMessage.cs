using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Discord;
using Discord.WebSocket;
using System.IO;

namespace CameraManager
{
    public partial class FormConfigMessage : Form
    {
        private DiscordSocketClient _discordClient;
        private bool _discordReady = false;
        private string _discordBotToken = "MTM5NzQyNjg5NTQ1MjI0NjAxNg.GB8PVN.Q7exGNICXiQlx2-wQxW0-qKr0y7lzoKCsYYtz4";
        private ulong _discordChannelId = 653602857060401207; // Replace with your channel ID
        private System.Windows.Forms.CheckBox chkEnableAlert;

        public FormConfigMessage()
        {
            InitializeComponent();
        }

        private void InitComboBoxes()
        {
            // Populate application selection combo box
            cmbAppSelect.Items.Clear();
            cmbAppSelect.Items.AddRange(new object[]
            {
                "Telegram",
                "Discord",
                "WhatsApp"
            });
            cmbAppSelect.SelectedIndex = ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage; // Default to Telegram

            // Populate message content combo box
            cmbMessageSelect.Items.Clear();
            cmbMessageSelect.Items.Add("Fire alarm!");
            cmbMessageSelect.Items.Add("Smoke alarm!"); // Added this line
            cmbMessageSelect.SelectedIndex = 0;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string selectedApp = cmbAppSelect.SelectedItem?.ToString();
            string message = cmbMessageSelect.SelectedItem?.ToString();


            if (selectedApp == "Telegram")
            {
                ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage = 0;
            }
            else if (selectedApp == "Discord")
            {
                ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage = 1;
            }
            else if (selectedApp == "WWhatsapp")
            {
                ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage = 2;
            }
            else
            {
                MessageBox.Show("Chức năng gửi cho ứng dụng này chưa được hỗ trợ.");
            }

            SaveDeviceConfig(true);
        }

        #region Save/Load Config
        public void SaveDeviceConfig(bool ShowMessage)
        {
            string file_name = Directory.GetCurrentDirectory() + @"\Config Setting\SendMessageConfig.ini";

            if (!System.IO.File.Exists(file_name))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Config Setting");
            }

            try
            {
                using (StreamWriter objWriter = new StreamWriter(file_name))
                {
                    // SAVING
                    objWriter.WriteLine("[SEND MESSAGE MODE]  " + (ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage));
                    objWriter.WriteLine("[ENABLE ALERT]  " + (chkEnableAlert.Checked ? "1" : "0"));
                    objWriter.Close();
                }
                if (ShowMessage)
                {
                    MessageBox.Show("Saved Success");
                }
            }
            catch
            {
                if (ShowMessage)
                {
                    MessageBox.Show("Save Fail");
                }
            }

        }
        private void LoadDeviceConfig(bool ShowMessage)
        {
            string file_name = Directory.GetCurrentDirectory() + @"\Config Setting\SendMessageConfig.ini";

            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\Config Setting"))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Config Setting");
            }
            else
            {
                try
                {
                    try
                    {
                        ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSendMessage = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "SEND MESSAGE MODE", "0"), 0);
                        ClassSystemConfig.Ins.m_ClsCommon.b_ByPassAlarm = Convert.ToInt32(ClassCommon.GetConfig(file_name, "ENABLE ALERT", "0"));
                        chkEnableAlert.Checked = (ClassSystemConfig.Ins.m_ClsCommon.b_ByPassAlarm == 1);
                    }
                    catch { }

                }
                catch
                {
                    if (ShowMessage)
                    {
                        MessageBox.Show("Load Fail");
                    }
                }
            }

        }

        #endregion

        private void FormConfigMessage_Load(object sender, EventArgs e)
        {
            LoadDeviceConfig(false);
            InitComboBoxes();

        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            string selectedApp = cmbAppSelect.SelectedItem?.ToString();
            string message = cmbMessageSelect.SelectedItem?.ToString();

            if (selectedApp == "Telegram")
            {
                await SendTelegramMessageAsync(message);
                MessageBox.Show("Đã gửi tin nhắn Telegram!");
            }
            else if (selectedApp == "Discord")
            {
                await SendDiscordMessageAsync(message);
                MessageBox.Show("Đã gửi tin nhắn Discord!");
            }
            else
            {
                MessageBox.Show("Chức năng gửi cho ứng dụng này chưa được hỗ trợ.");
            }
        }

        private async Task SendTelegramMessageAsync(string message)
        {
            // Thay YOUR_BOT_TOKEN và YOUR_CHAT_ID bằng thông tin thực tế
            string botToken = "7918989769:AAEAH2IAU91rJ3pBGGGLhuE2SDm03Q4-TH4";
            string chatId = "8178161299";
            string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";


            using (HttpClient client = new HttpClient())
            {
                await client.GetAsync(url);
            }
        }

        private async Task SendDiscordMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Vui lòng nhập nội dung!");
                return;
            }

            // Ensure the Discord client is initialized and connected
            if (_discordClient == null)
            {
                _discordClient = new DiscordSocketClient();
                _discordClient.Log += (msg) => { Console.WriteLine(msg); return Task.CompletedTask; };
                await _discordClient.LoginAsync(TokenType.Bot, _discordBotToken);
                await _discordClient.StartAsync();

                // Wait for the client to be ready
                var tcs = new TaskCompletionSource<bool>();
                _discordClient.Ready += () =>
                {
                    tcs.SetResult(true);
                    return Task.CompletedTask;
                };
                await tcs.Task;
            }

            var channel = _discordClient.GetChannel(_discordChannelId) as IMessageChannel;
            if (channel != null)
            {
                await channel.SendMessageAsync(message);
                Console.WriteLine("Đã gửi tin nhắn!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy channel!");
            }
        }
    }
}
