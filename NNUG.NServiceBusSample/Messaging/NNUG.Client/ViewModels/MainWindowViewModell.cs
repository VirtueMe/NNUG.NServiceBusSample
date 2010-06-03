using System;
using System.Collections.ObjectModel;
using Caliburn.Core.Metadata;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Filters;
using NNUG.Messages;
using NNUG.Prosjekt.Client.Events;
using NNUG.Prosjekt.Client.ViewModels.Interfaces;
using NServiceBus;

namespace NNUG.Prosjekt.Client.ViewModels
{
    [PerRequest(typeof(IMainWindowViewModel))]
    public class MainWindowViewModel : Presenter, IMainWindowViewModel, IListener<ProsjektAdded>
    {
        private string prosjekt;
        private string timer;
        private DateTime dato = DateTime.Now;
        private ObservableCollection<ProsjektAdded> collection = new ObservableCollection<ProsjektAdded>();

        public MainWindowViewModel(IBus bus)
        {
            Bus = bus;
        }

        public string Prosjekt
        {
            get
            {
                return prosjekt;
            }

            set
            {
                prosjekt = value;
                NotifyOfPropertyChange("Prosjekt");
            }
        }

        public ObservableCollection<ProsjektAdded> Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        public string Timer
        {
            get
            {
                return timer;
            }

            set
            {
                timer = value;
                NotifyOfPropertyChange("Timer");
            }
        }

        public DateTime Dato
        {
            get
            {
                return dato;
            }

            set
            {
                dato = value;
                NotifyOfPropertyChange("Dato");
            }
        }

        public IBus Bus { get; set; }
        

        public bool CanSend
        {
            get { return FelterInneholderData && Bus != null; }
        }

        protected bool FelterInneholderData
        {
            get { return FeltInneholderData(prosjekt) && FeltInneholderData(timer); }
        }

        [Dependencies("Prosjekt", "Timer")]
        [Preview("CanSend")]
        public void Send()
        {
            Bus.Send(Bus.CreateInstance<ProsjektMessage>((m) => 
                                                {
                                                    m.Navn = Prosjekt;
                                                    m.Antall = Timer;
                                                    m.Dato = Dato;
                                                }));
            
            Timer = string.Empty;
        }

        public void HandleMessage(ProsjektAdded message)
        {
            collection.Add(message);
        }

        private static bool FeltInneholderData(string felt)
        {
            return string.IsNullOrEmpty(felt) == false;
        }

    }
}
