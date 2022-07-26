using System;

namespace QuanLyKVC.Help
{
    public static class AutoIncreaseID
    {
        public static string IncreaseID(string maten, string IdLast, int TotalnumberOfId)
        {
            int flag = 0;
            string Id_New = maten;
            if (TotalnumberOfId <= 0) TotalnumberOfId = 1;
            if (IdLast != "")
            {
                string IdDocGia_Last = IdLast;
                int maso = Convert.ToInt32(IdDocGia_Last.Replace(maten, ""));
                string MaxId = "";
                for (int i = 0; i < TotalnumberOfId; i++) { MaxId += "9"; }// tao so max theo don vi
                if (maso < Convert.ToInt32(MaxId))
                {
                    for (int n = 10; maso >= n - 1; flag++, n *= 10) { }//dem don vi ma so
                    for (int i = 0; i < TotalnumberOfId - flag - 1; i++) { Id_New += "0"; }//tao chuoi 0
                }
                Id_New += maso + 1;
                return Id_New;
            }
            else
            {
                for (int i = 0; i < TotalnumberOfId - 1; i++)
                    Id_New += "0";
                return Id_New + "1";
            }
        }
    }
}
