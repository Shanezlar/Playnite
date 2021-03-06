﻿using NLog;
using Playnite;
using PlayniteUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayniteUI.ViewModels
{
    public class UpdateViewModel : ObservableObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IWindowFactory window;
        private Update update;

        public RelayCommand<object> CloseCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                CloseView();
            });
        }

        public RelayCommand<object> InstallUpdateCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                InstallUpdate();
            });
        }

        public List<Update.ReleaseNoteData> ReleaseNotes
        {
            get;
            private set;
        }

        public UpdateViewModel(Update update, IWindowFactory window)
        {
            this.window = window;
            this.update = update;
            ReleaseNotes = update.LatestReleaseNotes.OrderBy(a => a.Version).ToList();
        }

        public void OpenView()
        {
            window.CreateAndOpenDialog(this);
        }

        public void CloseView()
        {
            window.Close();
        }

        public void InstallUpdate()
        {
            update.InstallUpdate();
        }
    }
}
