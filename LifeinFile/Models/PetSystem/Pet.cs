using System;
using System.Windows;

namespace Model.PetSystem
{
    public class Pet
    {
        private Window targetWindow;
        private Window myCage;
        private PetMover mover;

        public Pet(Window window, Window cage)
        {
            targetWindow = window;
            myCage = cage;
            mover = new PetMover(targetWindow, myCage);
        }

        public void EatFiles(string[] files)
        {
            foreach (string file in files)
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                    file,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                );
            }
        }
    }
}