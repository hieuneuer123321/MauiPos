using MauiAppUIDemo.Services;

namespace MauiAppUIDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // ✅ Gán trước một trang tối giản (tránh lỗi)
            MainPage = new AppShell(); // ✅ Gán Shell luôn, không để ContentPage tạm nữa
        }

      
    }


}
