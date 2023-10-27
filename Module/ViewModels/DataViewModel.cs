﻿using Prism.Commands;
using Prism.Mvvm;
using PrismApplicationMavinwoo_Test.core.DataAccess;
using PrismApplicationMavinwoo_Test.core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Module.ViewModels
{

    internal class DataViewModel : BindableBase
    {
        // "_dataRepository" field is declared as an IDataRepository
        private IDataRepository _dataRepository;

        // "_title" field is declared as a "List" of type "OrderInfoModel"
        private List<OrderInfoModel> _title;

        // "Title" property is declared and the "getter" and "setter" are both created
        public List<OrderInfoModel> Title { get => _title; set => _title = value; }

        //private DateTime _testbox = new DateTime(1998,04,30);

        //  The original setter read: set { _testbox = value } 
        //  Needed to use SetProperty() for DelegateCommand to work
        //public DateTime Testbox 
        //{ 
        //    get { return _testbox; } 
        //    set { SetProperty(ref _testbox, value); } 
        //}

        private DateTime _date_Start;
        private DateTime _date_End;
        private List<OrderInfoModel> _filterData;
        private ObservableCollection<OrderInfoModel> _filterD;

        public DateTime Date_Start { get => _date_Start; set { SetProperty(ref _date_Start, value); } }
        public DateTime Date_End { get => _date_End; set { SetProperty(ref _date_End, value); } }
        public List<OrderInfoModel> FilterData { get => _filterData; set { SetProperty(ref _filterData, value); } }
        public DelegateCommand TestClick {  get; private set; }
        public DelegateCommand FilterDataResults { get; private set; }
        public ObservableCollection<OrderInfoModel> FilterD { get => _filterD; set { SetProperty(ref _filterD, value); } }



        // DataViewModel Constructor
        public DataViewModel(IDataRepository dataRepository)
        {
            
            // DataViewModel parameter "dataRepository" assigned to private field "_dataRepository"
            _dataRepository = dataRepository;
            
            // Title is now initialized with an instance of "List<OrderInfoModel"
            Title = new List<OrderInfoModel>();

            // "GetData()" clears anything within Title, then adds the info retrieved from GetData() query in _dataRepository
            //GetData();

            GetData();

       
            //TestClick = new DelegateCommand(Click, CanClick);

            FilterDataResults = new DelegateCommand(Filter, CanClick);
            Date_Start = DateTime.Now;
            Date_End = DateTime.Now;
            FilterData = new List<OrderInfoModel>();
            FilterD = new ObservableCollection<OrderInfoModel>();

        }

        private void GetData()
        {
            Title.Clear();
            Title.AddRange(_dataRepository.GetData());
        }

        public void Filter()
        {
            FilterD.Clear();
            FilterD.AddRange(_dataRepository.FilterData(Date_Start, Date_End));
        }

        private bool CanClick()
        {
            return true;
        }

        //private void Click()
        //{
        //    Title = FilterData;
        //}
    }
}