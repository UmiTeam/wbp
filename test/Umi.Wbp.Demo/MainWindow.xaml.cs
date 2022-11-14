using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Umi.Wbp.Commands;
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

        public MainWindow(IRouterService routerService){
            this.routerService = routerService;
            InitializeComponent();
            LoadedCommand = new RelayCommand(() => { routerService.Push("/home"); });
        }

        public ICollection<RouterView> RouterViews => new List<RouterView> { RouterView };
        public ICommand LoadedCommand { get; set; }
    }
}