using System;
using System.Windows;
using SimpleNotesClient.ViewModels;
using SimpleNotesClient.Views;

namespace SimpleNotesClient.Managers
{
    public static class WindowsManager
    {
        public static Window CurrentWindow { get; private set; }

        public static Window CurrentDialog { get; private set; }

        public static TContext SetWindow<TWindow, TContext>()
            where TWindow : Window, new()
            where TContext : new()
        {
            TContext context = new TContext();

            TWindow window = new TWindow();

            window.DataContext = context;

            window.Show();

            CloseCurrentWindowRaw();

            CurrentWindow = window;

            return context;
        }

        public static TContext SetWindow<TWindow, TContext>(Action<TContext> contextConfigure)
            where TWindow : Window, new()
            where TContext : new()
        {
            TContext context = SetWindow<TWindow, TContext>();

            contextConfigure(context);

            return context;
        }

        public static bool? ShowChangeOrCreateDialog(Action<ChangeOrCreateViewModel> contextConfigure)
        {
            ChangeOrCreate changeOrCreate = new ChangeOrCreate();

            ChangeOrCreateViewModel viewModel = new ChangeOrCreateViewModel();

            changeOrCreate.DataContext = viewModel;

            contextConfigure(viewModel);

            CurrentDialog = changeOrCreate;

            return changeOrCreate.ShowDialog();
        }

        public static void CloseCurrentWindowRaw()
        {
            CurrentWindow?.Close();
            CurrentWindow = default;
        }

        public static bool CloseCurrentWindow()
        {
            if (CurrentWindow != default)
            {
                CurrentWindow.Close();
                CurrentWindow = default;

                return true;
            }

            return false;
        }

        public static TContextResult GetContext<TContextResult>(this Window window) =>
            (TContextResult) window.DataContext;

        public static void SetMainWindow(MainWindow mainWindow) => CurrentWindow = mainWindow;
    }
}