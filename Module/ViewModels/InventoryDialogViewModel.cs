﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismApplicationMavinwoo_Test.core.DataAccess;
using PrismApplicationMavinwoo_Test.core.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ViewModels
{
    public class InventoryDialogViewModel : BindableBase, IDialogAware
    {
        private IDataRepository _dataRepository;
        private string _item;
        private int _reorderLimit;
        private DateTime? _deliveryDate;
        private int? _onOrder;
        private int _inStock;
        private int _displayInventoryIndex = 0;
        private int _displayAddIndex = 2;
        private int _displayDeleteIndex = 0;
        private int _displayUpdateIndex = 0;
        private string _selection;
        private ObservableCollection<InventoryAddDialogModel> _inventoryData;
        private ObservableCollection<string> _test;

        public string Item { get => _item; set => _item = value; }
        public int InStock { get => _inStock; set => _inStock = value; }
        public int? OnOrder { get => _onOrder; set => _onOrder = value; }
        public DateTime? DeliveryDate { get => _deliveryDate; set => _deliveryDate = value; }
        public int ReorderLimit { get => _reorderLimit; set => _reorderLimit = value; }
        public int DisplayInventoryIndex { get => _displayInventoryIndex; set { SetProperty(ref _displayInventoryIndex, value); } }
        public int DisplayAddIndex { get => _displayAddIndex; set { SetProperty(ref _displayAddIndex, value); } }
        public int DisplayDeleteIndex { get => _displayDeleteIndex; set { SetProperty(ref _displayDeleteIndex, value); } }
        public int DisplayUpdateIndex { get => _displayUpdateIndex; set { SetProperty(ref _displayUpdateIndex, value); } }
        public string Selection { get => _selection; set { SetProperty(ref _selection, value); } }
        public ObservableCollection<InventoryAddDialogModel> InventoryData { get => _inventoryData; set { SetProperty(ref _inventoryData, value); } }
        public ObservableCollection<string> Test { get => _test; set { SetProperty(ref _test, value); }  }
        public DelegateCommand DisplaySelectedCommand {  get; private set; }

        //private string _message;
        //public string Message
        //{
        //    get { return _message; }
        //    set { SetProperty(ref _message, value); }
        //}

        public InventoryDialogViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            DisplaySelectedCommand = new DelegateCommand(DisplaySelected);
            InventoryData = new ObservableCollection<InventoryAddDialogModel>();
            Test= new ObservableCollection<string>();

        }

        
        public void GetInvColumns()
        {
            Test.Clear();
            Test.AddRange(_dataRepository.GetColumns());
        }
        public void GetInvItems()
        {
            if (InventoryData.Any())
            {
                return;
            }

            var customers = _dataRepository.GetInventoryItems();

            if (customers is not null)
            {
                foreach(var customer in customers)
                {
                    InventoryData.Add(customer);
                }
            }
        }
        public void DisplaySelected()
        {
            switch (Selection)
            {
                case "System.Windows.Controls.ComboBoxItem: Add":
                    DisplayInventoryIndex = 0;
                    DisplayDeleteIndex = 0;
                    DisplayUpdateIndex = 0;
                    DisplayAddIndex = 2;
                    break;
                case "System.Windows.Controls.ComboBoxItem: Delete":
                    DisplayInventoryIndex = 0;
                    DisplayDeleteIndex = 2;
                    DisplayUpdateIndex = 0;
                    DisplayAddIndex = 0;
                    break;
                case "System.Windows.Controls.ComboBoxItem: Update":
                    DisplayInventoryIndex = 0;
                    DisplayDeleteIndex = 0;
                    DisplayUpdateIndex = 2;
                    DisplayAddIndex = 0;
                    GetInvItems();
                    GetInvColumns();
                    break;
                case "System.Windows.Controls.ComboBoxItem: Display":
                    DisplayInventoryIndex = 2;
                    DisplayDeleteIndex = 0;
                    DisplayUpdateIndex = 0;
                    DisplayAddIndex = 0;
                    GetInventoryData();
                    break;

            }
        }
        public void GetInventoryData()
        {
            InventoryData.Clear();
            InventoryData.AddRange(_dataRepository.GetInventory());
            InventoryData.ToList();
        }
        
        
        public string Title => "Edit Inventory";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }
        //private void CloseDialog()
        //{
        //    var result = ButtonResult.OK;

        //    var p = new DialogParameters();
        //    p.Add("myParam", "The dialog was closed by the user");

        //    RequestClose.Invoke(new DialogResult(result, p));
        //}
        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //Message = parameters.GetValue<string>("message");
        }
    }
}