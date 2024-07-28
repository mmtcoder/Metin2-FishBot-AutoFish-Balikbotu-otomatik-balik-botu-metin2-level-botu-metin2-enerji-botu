using Metin2AutoFishCSharp.Sources.ChatHandler;
using Metin2AutoFishCSharp.Sources.LevelAndFarms;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicPlayerApp.Sources.KeyboardInput;

namespace Metin2AutoFishCSharp.Sources.GameHandler
{
    internal class LevelHandle
    {

        private ImageObjects imagesObject;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;
    
        private AutoHunter autoHunter;
        private CharSpecialThings charThings;
        private GameAlphabetDetecter alphabetDetecter;
        private GameInputHandler inputs;
        private SkillsHandler skills;

        private TimerGame timerLevelDetection;

       private bool Is_Level_Detected;

        private Rectangle rectCurrentRedPotPos = Rectangle.Empty;
        private Rectangle rectCurrentBluePotPos = Rectangle.Empty;

        int[] currentRedPotIcon = null;
        int[] currentBluePotIcon = null;

        public LevelHandle(ImageObjects imagesObject)
        {
            this.imagesObject = imagesObject;
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);        
            charThings = new CharSpecialThings(imagesObject);
            alphabetDetecter = new GameAlphabetDetecter(imagesObject);
            
            autoHunter = new AutoHunter(imagesObject);
            timerLevelDetection = new TimerGame();
            inputs = new GameInputHandler();
            skills = new SkillsHandler();
        }

        public void StartLevelAndFarming()
        {
            if(ThreadGlobals.CanLevelAndFarmRightNow())
            {
                if(rectCurrentRedPotPos == Rectangle.Empty && rectCurrentBluePotPos == Rectangle.Empty)
                {
                    PreparingPots();
                }
                ControlAndFillTheHpAndSp();
                autoHunter.StartStopOtomatikAv();
                skills.StartSkillUsing();

                //DebugPfCnsl.println("AutoAv enable value = " + AutoHunter.IS_AUTO_HUNTER_STARTED);
               
                
            }
            else
            {
               // ThreadGlobals.DebugThreadGloablValues();
            }
            
        }

       

        public void StopOtomatikAv()
        {
            autoHunter.StopOtomatikAv();
        }

        /// <summary>
        /// checks the hp and sp values 
        /// </summary>
        /// <returns> int array which first index refers to hp and second is sp fullness ratio </returns>
        private int[] ControlHpAndManaBar()
        {
            int[] resultArray = new int[2];
            resultArray[0] = 0;
            resultArray[1] = 0;

            int[] targetHpEmptyBar = screenShot.ImageArraySpecifiedArea(coor.RectHpTitleBar());
            int detectedHpBarIndex = 0;

            int[] targetSpEmptyBar = screenShot.ImageArraySpecifiedArea(coor.RectManaTitleBar());
            int detectedSpBarIndex = 0;

            if (targetHpEmptyBar.Length == imagesObject.arrayEmptyBar.Length)
            {
                for (int titleX = coor.RectHpTitleBar().Width -1 ; titleX >= 0; titleX--)
                {
                    if (!imagesObject.CompareTwoRgbIntAdvanced(imagesObject.arrayEmptyBar[titleX],
                        targetHpEmptyBar[titleX]))
                    {
                        detectedHpBarIndex = titleX;
                        break;
                    }
                }
               // DebugPfCnsl.println("length = " + coor.RectHpTitleBar().Width + "  detectedHpBar = " + detectedHpBarIndex);
                float percentHpValue = ((float)((float)detectedHpBarIndex / (float)(coor.RectHpTitleBar().Width - 1)) * 100f);
               
               
                resultArray[0] = (int)percentHpValue;
               // Console.WriteLine("Şu anki hp değeri int = " + percentHpValue);
                
               // return resultArray;
            }
            if (targetSpEmptyBar.Length == imagesObject.arrayEmptyBar.Length)
            {
                for (int titleX = coor.RectManaTitleBar().Width - 1; titleX >= 0; titleX--)
                {
                    if (!imagesObject.CompareTwoRgbIntAdvanced(imagesObject.arrayEmptyBar[titleX],
                        targetSpEmptyBar[titleX]))
                    {
                        detectedSpBarIndex = titleX;
                        break;
                    }
                }
                // DebugPfCnsl.println("length = " + coor.RectHpTitleBar().Width + "  detectedHpBar = " + detectedHpBarIndex);
                float percentSpValue = ((float)((float)detectedSpBarIndex / (float)(coor.RectManaTitleBar().Width - 1)) * 100f);

                
                resultArray[1] = (int)percentSpValue;
                // Console.WriteLine("Şu anki hp değeri int = " + percentHpValue);
           
               // return resultArray;
            }
            else
            {
                DebugPfCnsl.println("İkisinin uzunluğu aynı değil");
            }
          //  DebugPfCnsl.println("Tespit edilen Hp ve Sp değerleri = ");
           // DebugPfCnsl.PrintArray(resultArray);

            return resultArray;
        }

        public bool PreparingPots()
        {
            DebugPfCnsl.println("PreparingPots is running");
            //First Check Red Pots
            Rectangle[] rectSmallRedPot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayRedPotKIcon
                ,coor.RectItemSlotSmallSizeSample());
            if (rectSmallRedPot != null && rectSmallRedPot.Length >0)
            {
                DebugPfCnsl.println("red küçük bulundu");
                rectCurrentRedPotPos = rectSmallRedPot[0];
                currentRedPotIcon = imagesObject.arrayRedPotKIcon;
            }
            else
            {
                Rectangle[] rectMediumRedPot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayRedPotOIcon
                    ,coor.RectItemSlotSmallSizeSample());
                if(rectMediumRedPot != null && rectMediumRedPot.Length >0 )
                {
                    DebugPfCnsl.println("red orat bulundu");
                    rectCurrentRedPotPos = rectMediumRedPot[0];
                    currentRedPotIcon = imagesObject.arrayRedPotOIcon;
                }
                else
                {
                    Rectangle[] rectBigRedPot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayRedPotBIcon
                        ,coor.RectItemSlotSmallSizeSample());
                    if (rectBigRedPot != null && rectBigRedPot.Length > 0)
                    {
                        DebugPfCnsl.println("red büyük bulundu");
                        rectCurrentRedPotPos = rectBigRedPot[0];
                        currentRedPotIcon = imagesObject.arrayRedPotBIcon;
                    }
                    else
                    {
                        Rectangle[] rectXxlRedPot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayRedPotXxlIcon
                            , coor.RectItemSlotSmallSizeSample());
                        if(rectXxlRedPot != null && rectXxlRedPot.Length > 0 )
                        {
                            DebugPfCnsl.println("red xxl bulundu");
                            rectCurrentRedPotPos = rectXxlRedPot[0];
                            currentRedPotIcon = imagesObject.arrayRedPotXxlIcon;
                        }
                        else
                        {
                            //Find red pot from inventory and insert skill slot place
                             if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayRedPotXxlIcon,
                                coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("red xxl çalıştı");


                                charThings.InsertObjectToSkillSlots(imagesObject.arrayRedPotXxlIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;
                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayRedPotBIcon,
                              coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("red büyük çalıştı");


                                charThings.InsertObjectToSkillSlots(imagesObject.arrayRedPotBIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;
                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayRedPotOIcon,
                             coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("red orta çalıştı");


                                charThings.InsertObjectToSkillSlots(imagesObject.arrayRedPotOIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;
                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayRedPotKIcon,
                                coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("red küçük çalıştı");
                                charThings.InsertObjectToSkillSlots(imagesObject.arrayRedPotKIcon,
                                    coor.RectItemSlotSmallSizeSample(),InsertCountSetting.INSERT_COUNT_1);
                                return true;
                            }
                          
                           
                            
                        }
                    }
                }
            }

            //Second Check Blue Pots

            Rectangle[] rectSmallBluePot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayBluePotKIcon,
                coor.RectItemSlotSmallSizeSample());
            if (rectSmallBluePot != null && rectSmallBluePot.Length > 0)
            {
                DebugPfCnsl.println("mavi küçük bulundu");
                rectCurrentBluePotPos = rectSmallBluePot[0];
                currentBluePotIcon = imagesObject.arrayBluePotKIcon;
            }
            else
            {
                Rectangle[] rectMediumBluePot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayBluePotOIcon
                    , coor.RectItemSlotSmallSizeSample());
                if (rectMediumBluePot != null && rectMediumBluePot.Length > 0)
                {
                    DebugPfCnsl.println("mavi orta bulundu");
                    rectCurrentBluePotPos = rectMediumBluePot[0];
                    currentBluePotIcon = imagesObject.arrayBluePotOIcon;
                }
                else
                {
                    Rectangle[] rectBigBLuePot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayBluePotBIcon
                        , coor.RectItemSlotSmallSizeSample());
                    if (rectBigBLuePot != null && rectBigBLuePot.Length > 0)
                    {
                        DebugPfCnsl.println("mavi büyük bulundu");
                        rectCurrentBluePotPos = rectBigBLuePot[0];
                        currentBluePotIcon = imagesObject.arrayBluePotBIcon;
                    }
                    else
                    {
                        Rectangle[] rectXxlBLuePot = charThings.CheckObjectFromSkillSlots(imagesObject.arrayBluePotXxlIcon
                            , coor.RectItemSlotSmallSizeSample());
                        if (rectXxlBLuePot != null && rectXxlBLuePot.Length > 0)
                        {
                            DebugPfCnsl.println("mavi xxl bulundu");
                            rectCurrentBluePotPos = rectXxlBLuePot[0];
                            currentBluePotIcon = imagesObject.arrayBluePotXxlIcon;
                        }
                        else
                        {
                            //Find blue pot from inventory and insert skill slot place
                             if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayBluePotXxlIcon,
                                coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("mavi xxl çalıştı");
                                charThings.InsertObjectToSkillSlots(imagesObject.arrayBluePotXxlIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;

                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayBluePotBIcon,
                               coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {

                                DebugPfCnsl.println("mavi büyük çalıştı");
                                charThings.InsertObjectToSkillSlots(imagesObject.arrayBluePotBIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;

                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayBluePotOIcon,
                               coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {

                                DebugPfCnsl.println("mavi orta çalıştı");
                                charThings.InsertObjectToSkillSlots(imagesObject.arrayBluePotOIcon,
                                    coor.RectItemSlotSmallSizeSample(), InsertCountSetting.INSERT_COUNT_1);
                                return true;

                            }
                            else if (charThings.CheckObjectTwoPageInventory(imagesObject.arrayBluePotKIcon,
                                coor.RectItemSlotSmallSizeSample()).Length > 1)
                            {
                                DebugPfCnsl.println("mavi k çalıştı");
                                charThings.InsertObjectToSkillSlots(imagesObject.arrayBluePotKIcon,
                                    coor.RectItemSlotSmallSizeSample(),InsertCountSetting.INSERT_COUNT_1);
                                return true;
                            }
                            
                            
                          
                        }
                    }
                }
            }

            if (rectCurrentRedPotPos == Rectangle.Empty || rectCurrentBluePotPos == Rectangle.Empty)
            {
                if(ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("PreparingPots fonksiyonu durduruldu");
                    return false;
                }
                return PreparingPots();
            }
            else
            {
                DebugPfCnsl.println("rectHPposValue = " + rectCurrentRedPotPos);
                DebugPfCnsl.println("rectSPposValue = " + rectCurrentBluePotPos);
                charThings.OpenCloseInventory(false);
                return true;
            }
           
        }

        public void ControlAndFillTheHpAndSp()
        {
            int[] controlHpSpValue = ControlHpAndManaBar();
            //Check hp empty rate value according to user decision
            if (controlHpSpValue[0] <= ThreadGlobals.HP_SP_RATE[0])
            {
                if (rectCurrentRedPotPos != Rectangle.Empty)
                {
                    //Check there is wanted red pot icon
                   // DebugPfCnsl.println("kırmızı can azaldı");
                    if (imagesObject.FindAllImagesOnScreen(currentRedPotIcon, coor.RectItemSlotSmallSizeSample()
                        , rectCurrentRedPotPos).Length > 0)
                    {
                        //Tespit edilen bot yeri ilk dört hızlı erişim yerindeyse
                        if (rectCurrentRedPotPos.X < coor.RectSkillSlotSecondPlace().X)
                        {
                            for (int firstSlot = 0; firstSlot < 4; firstSlot++)
                            {
                                Rectangle rectContainer = new Rectangle(coor.RectSkillSlotFirstPlace().X + (32 * firstSlot),
                                    coor.RectSkillSlotFirstPlace().Y -16, coor.RectSkillSlotFirstPlace().Width,
                                    coor.RectSkillSlotFirstPlace().Height +16);

                                //if (rectCurrentRedPotPos.X == coor.RectSkillSlotFirstPlace().X + (32 * firstSlot))
                                if(rectContainer.Contains(rectCurrentRedPotPos))
                                {
                                    while (controlHpSpValue[0] <= ThreadGlobals.HP_SP_RATE[0])
                                    {
                                        if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                                        {
                                            DebugPfCnsl.println("ControlAndFillTheHpAndSp is returned");
                                            return;
                                        }
                                        inputs.KeyPress(KeyboardInput.ScanCodeShort.KEY_1 + (short)firstSlot);
                                        controlHpSpValue = ControlHpAndManaBar();
                                    }                                 
                                    break;
                                }
                            }
                        }
                        else
                        {
                           // DebugPfCnsl.println("çalişmasi gerek");
                            for (int secondSlot = 0; secondSlot < 4; secondSlot++)
                            {
                                Rectangle rectContainer = new Rectangle(coor.RectSkillSlotSecondPlace().X + (32 * secondSlot),
                                    coor.RectSkillSlotSecondPlace().Y -16, coor.RectSkillSlotSecondPlace().Width,
                                    coor.RectSkillSlotSecondPlace().Height +16);

                               // if (rectCurrentRedPotPos.X == coor.RectSkillSlotSecondPlace().X + (32 * secondSlot))
                               if(rectContainer.Contains(rectCurrentRedPotPos))
                                {
                                    // DebugPfCnsl.println("çalişmasi basmasıdır");
                                    while (controlHpSpValue[0] <= ThreadGlobals.HP_SP_RATE[0])
                                    {
                                        if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                                        {
                                            DebugPfCnsl.println("ControlAndFillTheHpAndSp is returned");
                                            return;
                                        }
                                        inputs.KeyPress(KeyboardInput.ScanCodeShort.F1 + (short)secondSlot);
                                        controlHpSpValue = ControlHpAndManaBar();
                                    }
                                    
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        DebugPfCnsl.println("kırmızı pot bitmiş tekrar PreparingPots çalıştırılıyor");
                        PreparingPots();
                    }

                }
                else
                {
                    DebugPfCnsl.println("kırmızı pot tespit edilmemiş büyük ihtimal envanterde pot kalmamış ");
                    
                }
            }
            //Check sp empty rate value according to user decision
            if (controlHpSpValue[1] <= ThreadGlobals.HP_SP_RATE[1])
            {
                if (rectCurrentBluePotPos != Rectangle.Empty)
                {
                    //Check there is wanted blue pot icon

                    if (imagesObject.FindAllImagesOnScreen(currentBluePotIcon, coor.RectItemSlotSmallSizeSample()
                        , rectCurrentBluePotPos).Length > 0)
                    {
                        //Tespit edilen bot yeri ilk dört hızlı erişim yerindeyse
                        if (rectCurrentBluePotPos.X < coor.RectSkillSlotSecondPlace().X)
                        {
                            for (int firstSlot = 0; firstSlot < 4; firstSlot++)
                            {
                                Rectangle rectContainer = new Rectangle(coor.RectSkillSlotFirstPlace().X + (32 * firstSlot),
                                   coor.RectSkillSlotFirstPlace().Y -16, coor.RectSkillSlotFirstPlace().Width,
                                   coor.RectSkillSlotFirstPlace().Height +16);

                                //if (rectCurrentBluePotPos.X == coor.RectSkillSlotFirstPlace().X + (32 * firstSlot))
                                if(rectContainer.Contains(rectCurrentBluePotPos))
                                {
                                    while (controlHpSpValue[1] <= ThreadGlobals.HP_SP_RATE[1])
                                    {
                                        if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                                        {
                                            DebugPfCnsl.println("ControlAndFillTheHpAndSp is returned");
                                            return;
                                        }
                                        inputs.KeyPress(KeyboardInput.ScanCodeShort.KEY_1 + (short)firstSlot);
                                        controlHpSpValue = ControlHpAndManaBar();
                                    }
                                  
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int secondSlot = 0; secondSlot < 4; secondSlot++)
                            {
                                Rectangle rectContainer = new Rectangle(coor.RectSkillSlotSecondPlace().X + (32 * secondSlot),
                                   coor.RectSkillSlotSecondPlace().Y -16, coor.RectSkillSlotSecondPlace().Width,
                                   coor.RectSkillSlotSecondPlace().Height +16);

                              //  if (rectCurrentBluePotPos.X == coor.RectSkillSlotSecondPlace().X + (32 * secondSlot))
                              if(rectContainer.Contains(rectCurrentBluePotPos))
                                {
                                    while (controlHpSpValue[1] <= ThreadGlobals.HP_SP_RATE[1])
                                    {
                                        if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                                        {
                                            DebugPfCnsl.println("ControlAndFillTheHpAndSp is returned");
                                            return;
                                        }
                                        inputs.KeyPress(KeyboardInput.ScanCodeShort.F1 + (short)secondSlot);
                                        controlHpSpValue = ControlHpAndManaBar();
                                    }                                 
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        DebugPfCnsl.println("mavi pot bitmiş tekrar PreparingPots çalıştırılıyor");
                        PreparingPots();
                    }

                }
                else
                {
                    DebugPfCnsl.println("mavi pot tespit edilmemiş büyük ihtimal envanterde pot kalmamış ");
                    
                }
            }
            
        }
        public int CheckCharacterLevel()
        {
            if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("CheckCharacterLevel function is returned old level value game is stopped");
                return CharInfo.CharLevel;
            }
            
            if (!Is_Level_Detected)
            {
             string result = alphabetDetecter.DetectGameText(ColorGame.MAP_PLAYER_LEVEL_GREEN,
                    coor.RectCharLevelDetectionArea());
                if (result != null && result != string.Empty)
                {
                    string[] parseString = result.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);

                    if (parseString.Length == 2)
                    {
                        //İlk tespit edilen indeks 'Lv' yazisi diğeri ise sayıdır
                        if(int.TryParse(parseString[1],out int value))
                        {
                            Is_Level_Detected = true;
                            timerLevelDetection.SetStartedSecondTime();

                            CharInfo.CharLevel = value;
                            return value;
                        }
                    }
                    else
                    {
                        DebugPfCnsl.println("ayrıştırma işleminde problem olabilir sonuç = " + result);
                        return CharInfo.CharLevel;
                    }
                }
                else
                {
                    //belki bakış açısında hata olabilir
                    charThings.BirdViewPerpective();
                }

                /*if (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayStatusImproveWhiteTitle,
               targetLevelGreen, ImageSensibilityLevel.SENSIBILTY_HIGH))
                {

                    isLevelDetected = true;
                    timerLevelDetection.SetStartedMilliSecTime();
                    
                }*/
            }
            else
            {
                if (timerLevelDetection.CheckDelayTimeInSecond(60))
                {
                    //DebugPfCnsl.println("2 saniyenin içinde ");
                    
                }
                else
                {
                    // DebugPfCnsl.println("2 saniye geçti ");
                    Is_Level_Detected = false;
                    //timerLevelDetection.SetStartedMilliSecTime();

                }
            }


            return CharInfo.CharLevel; 
        }
  
        public AutoHunter GetAutoHunter()
        {
            return autoHunter;
        }
    }
}
