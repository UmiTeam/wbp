using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Message;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRouterHost, IViewModelForSelf
    {
        private readonly IRouterService routerService;
        private readonly IMessageService messageService;

        public MainWindow(IRouterService routerService, IMessageService messageService){
            this.routerService = routerService;
            this.messageService = messageService;
            InitializeComponent();
            LoadedCommand = new RelayCommand(() =>
            {
                // routerService.Push("//home");
            });
            BackCommand = new RelayCommand((() => { routerService.GoBack(); }));
            ForwardCommand = new RelayCommand((() => { routerService.GoForward(); }));
            ShowMessageCommand = new RelayCommand(() => { messageService.ShowMessage($"Time: {DateTime.Now} Random: {Random.Shared.Next(int.MaxValue)}"); });
        }

        public MessageQueue MessageQueue => messageService.MessageQueue;
        public ICollection<RouterView> RouterViews => new List<RouterView> { RouterView };
        public ICommand LoadedCommand { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand ForwardCommand { get; set; }

        public ICommand ShowMessageCommand { get; set; }
    }
}