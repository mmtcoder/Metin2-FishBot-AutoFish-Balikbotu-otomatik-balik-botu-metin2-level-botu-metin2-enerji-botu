using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Metin2AutoFishCSharp.Sources
{
    internal class TelegramBot
    {

        private readonly string telegramToken = "Fill your token";
        //and you can change your telegram bot account username from
        //MainForm Designer "Haberlesme ve Diger" section

        public static Message TELEGRAM_SEND_MESSAGE = null;

        public static bool TELEGRAM_BOT_IS_READY = false;
        public static bool TELEGRAM_DIALOG_PANEL_ACTIVE = false;

        static TelegramBotClient botClient;
        private static ScreenShotWinAPI screenShot;
        private static ImageObjects imagesObject;
        private static GameObjectCoordinates coor;

        private GameInputHandler inputs;
        
        public TelegramBot(ImageObjects imageObject)
        {
            botClient = new TelegramBotClient(telegramToken);
            imagesObject = imageObject;
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imageObject);
            inputs = new GameInputHandler();
            StartReceiver();
        }

        public async Task StartReceiver()
        {
            var token = new CancellationTokenSource();
            var cancelToken = token.Token;
            var reOpt = new ReceiverOptions { AllowedUpdates = { } };
            await botClient.ReceiveAsync(OnMessage, ErrorMessage, reOpt, cancelToken);
        }

        //Receiver Message From Bot
        public async Task OnMessage(ITelegramBotClient botClient, Update update,
            CancellationToken cancelToken)
        {
            if (ThreadGlobals.isTelegramBotActive)
            {
                if (!TELEGRAM_BOT_IS_READY)
                {
                    if (!TELEGRAM_DIALOG_PANEL_ACTIVE)
                    {
                        if (update.Message is Message message)
                        {
                            //  DebugPfCnsl.println(message.Chat.Id + " message id");
                            // await botClient.SendTextMessageAsync(message.Chat.Id, "Test both");
                            if (message.Text == "/start")
                            {
                                TELEGRAM_SEND_MESSAGE = message;

                                await botClient.SendTextMessageAsync(message.Chat.Id, "Bilgileriniz alındı ve 'Test' butonuna " +
                                    "basıp çıkan dialog ekranına EVET diyiniz. Chat id değeriniz = "
                                    + message.Chat.Id + " adınız = " + message.Chat.FirstName);
                            }
                            else
                            {
                                TELEGRAM_SEND_MESSAGE = message;
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Bilgileriniz alındı ve 'Test' " +
                                    " butonuna basıp çıkan dialog ekranına EVET diyiniz. " +
                                    message.Chat.FirstName);
                            }
                        }
                    }

                }
                else
                {
                    if (update.Message is Message message)
                    {
                        if (message.Text.Equals("Durdur"))
                        {
                            inputs.KeyDown(KeyboardInput.ScanCodeShort.LCONTROL);
                            inputs.KeyDown(KeyboardInput.ScanCodeShort.KEY_O);
                            TimerGame.SleepRandom(30, 50);
                            inputs.KeyRelease(KeyboardInput.ScanCodeShort.LCONTROL);
                            inputs.KeyRelease(KeyboardInput.ScanCodeShort.KEY_O);

                            SendMessageTelegram("Program Durduruluyor.");
                        }
                    }
                }
            }
           
           
        }
        //Error Message
        public async Task ErrorMessage(ITelegramBotClient botClient, Exception e,
            CancellationToken cancelToken)
        {
            if (e is ApiRequestException requestException)
            {
                await botClient.SendTextMessageAsync("", e.Message.ToString());
            }
        }

        //Send Message 
        public static async Task SendMessageTelegram(string text)
        {
            if (ThreadGlobals.isTelegramBotActive)
            {
                if (TELEGRAM_SEND_MESSAGE != null)
                {
                   // Bitmap image = screenShot.CaptureSpecifiedScreen(new Rectangle(0,0,500,500));
                   // FileHandler.SaveImageAsPng(image,"testimage.png",PathWayStruct.PATH_SCREENSHOTS);

                   // string path = FileHandler.ReturnOrFindPathWay("testimage.png", PathWayStruct.PATH_SCREENSHOTS, ReturOrFind.RETURN_PATH);
                  //  await botClient.SendPhotoAsync(TELEGRAM_SEND_MESSAGE.Chat.Id, InputFile.FromFileId(path));

                    await botClient.SendTextMessageAsync(TELEGRAM_SEND_MESSAGE.Chat.Id, text);

                }
                else
                {
                    DebugPfCnsl.println("TELEGRAM gönderilecek kişinin id bilgisi kayıt yapılmalı.. ");
                }
            }
           
                   
        }
    }

}
