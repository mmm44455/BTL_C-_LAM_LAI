using Microsoft.Data.SqlClient;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        TelegramBotClient botClient;
      
        void AddLog(string msg)
        {
            if (txtlog.InvokeRequired)
            {
                txtlog.BeginInvoke((MethodInvoker)delegate ()
                {
                    AddLog(msg);
                });
            }
            else
            {
                txtlog.AppendText(msg + "\r\n");
            }
        }
        public Form1()
        {
            InitializeComponent();
            string token = "6088087317:AAHdYyeQdyF7LeiQlIi4DKHFfok9jm5REY0";
            botClient = new TelegramBotClient(token);

            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botClient.StartReceiving(
               updateHandler: HandleUpdateAsync,  //hàm xử lý khi có người chát đến
               pollingErrorHandler: HandlePollingErrorAsync,
               receiverOptions: receiverOptions,
               cancellationToken: cts.Token
           );

            Task<User> me = botClient.GetMeAsync();
            AddLog($"Bot begin working: @{me.Result.Username}");
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                bool ok = false;
                if (update.Message is not { } message)
                    return;
                // Only process text messages
                if (message.Text is not { } messageText)
                    return;

                var chatId = message.Chat.Id;
                

                AddLog($"{chatId}: {messageText}");

                string reply = "";
               if (messageText.StartsWith("/xoa_du_lieu"))
                {
                   
                     reply = "Đã xóa dữ liệu cũ.";
                    await botClient.SendTextMessageAsync(chatId, reply);
                    return;
                }
                if (messageText.StartsWith("tkiem"))
                {
                    string q = messageText.Substring(8);
                   reply = await Class1.TimKiemAsync(q);
                    if (string.IsNullOrEmpty(reply))
                    {
                        reply = "Không có dữ liệu ";
                    }
                    else
                    {
                        reply = "Kết quả tìm kiếm:\n" + reply;
                    }


                }
                else if (messageText.StartsWith("xoa"))
                {
                    string q = messageText.Substring(8);
                    reply = await Class1.xoaAsync(q);
                    reply = "xoa thanh cong";
                    if (string.IsNullOrEmpty(reply))
                    {
                        reply = "hong r ";
                    }

                }
                else if (messageText.StartsWith("them "))
                {
                    string ht = messageText.Substring(8);
                    string que = messageText.Substring(8);
                    string MS = messageText.Substring(8);
                    reply = await Class1.them(ht, que,MS);
                    reply = "them thanh cong";

                }
                else
                {
                    reply = " Tra loi: " +messageText;
                }
               
               
               


                // Echo received message text
                Telegram.Bot.Types.Message sentMessage = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: reply,
                       parseMode: ParseMode.Html  //bổ xung ngày 25.5.2023
                      );

            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
        }

        private void txtlog_TextChanged(object sender, EventArgs e)
        {

        }
    }


}