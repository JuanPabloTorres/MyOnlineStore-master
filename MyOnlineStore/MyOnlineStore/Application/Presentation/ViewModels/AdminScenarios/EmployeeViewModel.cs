using Microsoft.AspNetCore.SignalR.Client;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.EmployeeStepsView;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.AdminScenarios
{

    /*
     Actualizar empleado con las nuevas configuraciones.
         
         */

    [QueryProperty("EmployeeID", "employeeID")]
    public class EmployeeViewModel : BaseViewModel, IEmployeeViewModel
    {
        ObservableCollection<EmployeePresenter> employees;
        public ObservableCollection<EmployeePresenter> Employees
        {
            get => employees;
            set
            {
                employees = value;
                RaisePropertyChanged(() => Employees);
            }
        }

        private List<EmployeeWorkHour> EmployeeWorkHours;



        #region Commands Regions
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ICommand ConfigureEmployeeCommand { get; set; }
        public ICommand SwipeRequestCommand { get; set; }

        public ICommand EditCommand { get; set; }

        #endregion


        string storeid;
        public string StoreId
        {

            get => storeid;

            set
            {
                storeid = value;

                //var employeeData =   EmployeeDataStore.GetStoreEmployees(storeid);

                //Employees = new ObservableCollection<StoresEmployees>(employeeData);

                RaisePropertyChanged(() => StoreId);
            }
        }


        #region DataStore Region
        public IStoresEmployeesDataStore EmployeeDataStore { get; set; }

        IStoreDataStore StoreDataStore;

        public IUserDataStore UserDataStore { get; set; }

        #endregion
        public string employeename { get; set; }
        public string EmployeeName 
        { 
            get =>employeename; 
            set 
            {
                employeename = value; RaisePropertyChanged(() => EmployeeName); 
            }
        }

        string employeephone;
        public string EmployeePhoneNumber { get => employeephone;
            set 
            {
                employeephone = value;
                RaisePropertyChanged(() => EmployeePhoneNumber);
            
            } 
            
        }

        string email;
        public string EmployeeEmail { get =>email;
            set
            {
                email = value;
                RaisePropertyChanged(() => EmployeeEmail);
            }
        }



        TimeSpan starttime;
        public TimeSpan startTime
        {
            get => starttime;
            set 
            {

                starttime = value;
                RaisePropertyChanged(() => startTime);
            } 
        }


        TimeSpan endtime;
        public TimeSpan EndTime 
        { 
            get =>endtime; 
            set
            {

                endtime = value;
                RaisePropertyChanged(() => EndTime);
            }
                
        }

     

        public string searchvalue;
        public string SearchValue
        {
            get => searchvalue;

            set
            {
                searchvalue = value;
                RaisePropertyChanged(() => SearchValue);
            }
        }

        private string employeeid;

        public string EmployeeID
        {
            get { return employeeid; }
            set { employeeid = value; }
        }



        #region Day Checked Properties

        private bool ismondaychecked;

        public bool IsMondayChecked
        {
            get { return ismondaychecked; }
            set {
                ismondaychecked = value;
                RaisePropertyChanged(() => IsMondayChecked);

               
             


               
            }
        }

        private bool istuesdaychecked;

        public bool IsTuesdayChecked
        {
            get { return istuesdaychecked; }
            set
            {
                istuesdaychecked = value;
                RaisePropertyChanged(() => IsTuesdayChecked);
            }
        }

        bool iswednesdaychecked;
        public bool IsWednesdayChecked
        {
            get { return iswednesdaychecked; }
            set
            {
                iswednesdaychecked = value;
                RaisePropertyChanged(() => IsWednesdayChecked);
            }
        }

        bool isthuersdaychecked;
        public bool IsThuersdayChecked
        {
            get { return isthuersdaychecked; }
            set
            {
                isthuersdaychecked = value;
                RaisePropertyChanged(() => IsThuersdayChecked);
            }
        }

        private bool isfridaychecked;

        public bool IsFridayChecked
        {
            get { return isfridaychecked; }
            set { isfridaychecked = value;
                RaisePropertyChanged(() => IsFridayChecked);
            }
        }


        bool issaturdaychecked;
        public bool IsSaturdayChecked
        {
            get { return issaturdaychecked; }
            set
            {
                issaturdaychecked = value;
                RaisePropertyChanged(() => IsSaturdayChecked);
            }
        }

        private bool issundaychecked;

        public bool IsSundayChecked
        {
            get { return issundaychecked; }
            set { issundaychecked = value;
                RaisePropertyChanged(() => IsSundayChecked);
            }
        }

        #endregion

        #region Day Hour Properties

        private TimeSpan mondayhour;

        public TimeSpan MondayHour
        {
            get { return mondayhour; } // put a breakpoint here
            set { mondayhour = value;
                RaisePropertyChanged(() => MondayHour);
            } // put a breakpoint here
        }

        private TimeSpan mondayhourto;

        public TimeSpan MondayHourTo
        {
            get { return mondayhourto; } // put a breakpoint here
            set
            {
                mondayhourto = value;
                RaisePropertyChanged(() => MondayHourTo);
            } // put a breakpoint here
        }

        private TimeSpan tuesdayhour;

        public TimeSpan TuesdayHour 
        {
            get { return tuesdayhour; } 
            set
            {
                tuesdayhour = value;
                RaisePropertyChanged(() => TuesdayHour);
            } // put a breakpoint here
        }

        private TimeSpan tuesdayhourto;

        public TimeSpan TuesdayHourTo
        {
            get { return tuesdayhourto; }
            set
            {
                tuesdayhourto = value;
                RaisePropertyChanged(() => TuesdayHourTo);
            } // put a breakpoint here
        }

        private TimeSpan wednesdayhour;

        public TimeSpan WednesdayHour 
        {
            get { return wednesdayhour; } 
            set
            {
                wednesdayhour = value;
                RaisePropertyChanged(() => WednesdayHour);
            } // put a breakpoint here
        }

        private TimeSpan wednesdayhourto;

        public TimeSpan WednesdayHourTo
        {
            get { return wednesdayhourto; }
            set
            {
                wednesdayhourto = value;
                RaisePropertyChanged(() => WednesdayHourTo);
            } // put a breakpoint here
        }

        private TimeSpan thuersdayhour;

        public TimeSpan ThuersdayHour 
        {
            get { return thuersdayhour; } 
            set
            {
                thuersdayhour = value;
                RaisePropertyChanged(() => ThuersdayHour);
            } // put a breakpoint here
        }

        private TimeSpan thuersdayhourto;

        public TimeSpan ThuersdayHourTo
        {
            get { return thuersdayhourto; }
            set
            {
                thuersdayhourto = value;
                RaisePropertyChanged(() => ThuersdayHourTo);
            } // put a breakpoint here
        }

        private TimeSpan fridayhour;

        public TimeSpan FridayHour
        {
            get { return fridayhour; }
            set
            {
                fridayhour = value;
                RaisePropertyChanged(() => FridayHour);
            } // put a breakpoint here
        }

        private TimeSpan fridayhourto;

        public TimeSpan FridayHourTo
        {
            get { return fridayhourto; }
            set
            {
                fridayhourto = value;
                RaisePropertyChanged(() => FridayHourTo);
            } // put a breakpoint here
        }

        private TimeSpan saturdayhour;

        public TimeSpan SaturdayHour
        {
            get { return saturdayhour; }
            set
            {
                saturdayhour = value;
                RaisePropertyChanged(() => SaturdayHour);
            } 
        }

        private TimeSpan saturdayhourto;

        public TimeSpan SaturdayHourTo
        {
            get { return saturdayhourto; }
            set
            {
                saturdayhourto = value;
                RaisePropertyChanged(() => SaturdayHourTo);
            }
        }

        private TimeSpan sundayhour;

        public TimeSpan SundayHour
        {
            get { return sundayhour; }
            set
            {
                sundayhour = value;
                RaisePropertyChanged(() => SundayHour);
            }
        }

        private TimeSpan sundayhourto;

        public TimeSpan SundayHourTo
        {
            get { return sundayhourto; }
            set
            {
                sundayhourto = value;
                RaisePropertyChanged(() => SundayHourTo);
            }
        }



        #endregion

        #region Positions Properties

        private List<string> positions;

        public List<string> Positions
        {
            get { return positions; }
            set { positions = value;
                RaisePropertyChanged(() => Positions);
            }
        }

        private string  selectedposition;

        public string  SelectedPosition
        {
            get { return selectedposition; }
            set 
            {
                selectedposition = value;
                RaisePropertyChanged(() => SelectedPosition);
            }
        }

         EmployeePresenter detailemployee;

        public EmployeePresenter DetailEmployee
        {
            get => detailemployee;

            set
            {
                detailemployee = value;
                RaisePropertyChanged(() => DetailEmployee);
            }
        }


        private ObservableCollection<EmployeeWorkHour> detailemployeeworkhours;

        public ObservableCollection<EmployeeWorkHour> DetailEmployeeWorkHours
        {
            get { return detailemployeeworkhours; }
            set
            {
                detailemployeeworkhours = value;
                RaisePropertyChanged(() => DetailEmployeeWorkHours);
            }
        }




        #endregion

     

        ObservableCollection<User> user;
        public ObservableCollection<User> Users { get =>user; 
            set
            {

                user = value;
                RaisePropertyChanged(() => Users);
            } }

     

        private double swipeoffset;

        public double SwipeOffset
        {
            get { return swipeoffset; }
            set { swipeoffset = value;

                SwipeOffset = value;
                RaisePropertyChanged(() => SwipeOffset);
            }
        }

        
        public EmployeeViewModel(IStoresEmployeesDataStore employeeDataStore, IUserDataStore userDataStore,IStoreDataStore storeDataStore)
        {
            EmployeeDataStore = employeeDataStore;
            UserDataStore = userDataStore;
            StoreDataStore = storeDataStore;

            SetsPositios();

            Employees = new ObservableCollection<EmployeePresenter>();
           // DetailEmployeeWorkHours = new ObservableCollection<EmployeeWorkHour>();
            GetStoreEmployees();


            //Navega a searchemployee page
            SearchEmployeeCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new SearchEmployeePage());
            });

            //Envia un job request id a un determinado user
            SwipeRequestCommand = new Command<CommandEventData>(async (data) =>
            {
                var eventArgs = data.Args as Syncfusion.ListView.XForms.SwipeEndedEventArgs;

                var item = eventArgs.ItemData as User;

                if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (item != null)
                        {
                            JobRequest employ_request = new JobRequest()
                            {
                                ExpDate = DateTime.Today.AddDays(1),
                                Id = Guid.NewGuid(),
                                UserSenderId = App.ApplicationManager.CurrentStore.OwnerUserId,
                                UserReciverId = item.Id,
                                StoreId = App.ApplicationManager.CurrentStore.Id,
                                SenderStoreName = App.ApplicationManager.CurrentStore.StoreName,
                                SenderFullName = App.ApplicationManager.CurrentStore.StoreOwnerName,
                                RequestAnwser = Shared.Models.Entries.RequestFlag.None
                            };


                            var result = await UserDataStore.SendRequest(employ_request);

                            await ShowPopUp(result);


                        }
                    }
                }
            });


            //Busca un determinado user en la base de datos
            SearchCommand = new Command(async () =>
            {

                if (!string.IsNullOrEmpty(SearchValue))
                {
                    var userdata =await UserDataStore.GetUser(SearchValue);

                    Users = new ObservableCollection<User>(userdata);
                }


            });

            //Inserta la configuraciones dadas por el usuario al empleado seleccionado
            ConfigureEmployeeCommand = new Command(async() =>
            {
                ValidationAndSetEmployeeWorkHour();

                if (EmployeeWorkHours.Count > 0)
                {

                    storeDataStore.AddEmployeeWorkHours(EmployeeWorkHours);
                }

                var employee = StoreDataStore.GetStoreEmployee(EmployeeID, App.ApplicationManager.CurrentStore.Id.ToString());


                var result = StoreDataStore.UpdateEmployee(UpdateEmployee(employee));
                if (result)
                {
                    App.Current.MainPage.DisplayAlert("Notification", "Succefully Update", "OK");
                    GetStoreEmployees();
                    await Shell.Current.GoToAsync($"{EmployeeHomePage.Route}?" + $"storeID={App.ApplicationManager.CurrentStore!.Id.ToString()}");


                }

            });

            //Ira a la configuracion del empleado para editar el empleado seleccionado
            EditCommand = new Command(async () => 
            {
                await Shell.Current.GoToAsync($"{ConfigureEmployeePage.Route}?" + $"employeeID={DetailEmployee.UserEmployeeId.ToString()}");


            });


            //Obtiene los cambios automatico de los request acceptados.
            App.HubManager.GetHubConnection.On<string>("RequestAccepted", (request) =>
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {

                        AcceptedEmployeeRequest(request);
                       
                    }
                    catch (Exception ex)
                    {
                        App.Current.MainPage.DisplayAlert("Notification Error", ex.Message, "OK");
                    }
                });


            });

            //Remueve un usuario de la base de datos.
            MessagingCenter.Subscribe<EmployeePresenter, EmployeePresenter>(this, "ToRemove", (sender, arg) =>
            {

                var result = Employees.Remove(arg);

                if(result)
                {
                    App.Current.MainPage.DisplayAlert("Notification", "Employee removed", "OK");
                }

                //var tempdata = JobRequestPreseters;

                //JobRequestPreseters = new ObservableCollection<JobRequestPresenter>(tempdata);
            });

            //Coje informacion en detail view del employee seleccionado
            MessagingCenter.Subscribe<EmployeePresenter, EmployeePresenter>(this, "Detail", (sender, arg) =>
            {

                DetailEmployee = arg;

                DetailEmployeeWorkHours = new ObservableCollection<EmployeeWorkHour>();
                foreach (var item in arg.EmployeeWork)
                {
                    DetailEmployeeWorkHours.Add(item);
                }


            });

        

           


        }


        #region Methods

        void ValidationAndSetEmployeeWorkHour()
        {
            Guid convert_employeestring_id = Guid.Parse(EmployeeID);

            EmployeeWorkHours = new List<EmployeeWorkHour>();

            if (IsMondayChecked)
            {

              
                var employeeconfiguremonday = new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Monday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = MondayHourTo,
                    Start = MondayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfiguremonday);
            }
            if (IsTuesdayChecked)
            {
                var employeeconfiguretuesday= new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Tuesday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = TuesdayHourTo,
                    Start = TuesdayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfiguretuesday);
            }
            if (IsWednesdayChecked)
            {
                var employeeconfigurewednesday= new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Wednesday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = WednesdayHourTo,
                    Start = WednesdayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfigurewednesday);
            }
            if (IsThuersdayChecked)
            {
                var employeeconfigurethuersday = new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Thursday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = ThuersdayHourTo,
                    Start = ThuersdayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfigurethuersday);
            }
            if (IsFridayChecked)
            {
                var employeeconfigurefriday = new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Friday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = FridayHourTo,
                    Start = FridayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfigurefriday);
            }
            if (IsSaturdayChecked)
            {
                var employeeconfiguresaturday = new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Saturday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = SaturdayHourTo,
                    Start = SaturdayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfiguresaturday);
            }
            if (IsSundayChecked)
            {
                var employeeconfiguresunday = new EmployeeWorkHour()
                {
                    Day = DayOfWeek.Sunday.ToString(),
                    EmployeeId = convert_employeestring_id,
                    End = SundayHourTo,
                    Start = SundayHour,
                    StoreId = App.ApplicationManager.CurrentStore.Id,
                    WorkHourId = Guid.NewGuid()
                };

                EmployeeWorkHours.Add(employeeconfiguresunday);
            }

        }
        StoresEmployees UpdateEmployee(StoresEmployees employee)
        {

            StoresEmployees storesEmployeesupdated = new StoresEmployees()
            {
                Id = employee.Id,
                EmployeePosition = (Position)Enum.Parse(typeof(Position), SelectedPosition),
                FullName = employee.FullName,
                StoreId = employee.StoreId,
                UserEmployeeId = employee.UserEmployeeId,
                 
                IsAlive = employee.IsAlive,


            };

            return storesEmployeesupdated;

        }

        void SetsPositios()
        {
            Positions = new List<string>();

            Positions.Add(Position.Delivery.ToString());
            Positions.Add(Position.Preparer.ToString());
            Positions.Add(Position.Receptionist.ToString());
            Positions.Add(Position.Supervisor.ToString());
            Positions.Add(Position.Manager.ToString());
        }

     void  GetEmployeeWorkHours(EmployeePresenter arg)
        {
            var workhours = StoreDataStore.GetEmployeeWorkHoursOfStore(arg.UserEmployeeId.ToString(), arg.StoreId.ToString());

        }

        async  void GetStoreEmployees()
        {
            var employeeData =await  EmployeeDataStore.GetStoreEmployees(App.ApplicationManager.CurrentStore.Id.ToString());

            Employees = new ObservableCollection<EmployeePresenter>();

            foreach (var item in employeeData)
            {

                var employeePresenter = new EmployeePresenter(item, StoreDataStore);

                Employees.Add(employeePresenter);

            }

          
        }

        async Task ShowPopUp(bool result)
        {
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Notification", "Request sended successfully.", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Notification", "Request  was sended is waiting for response..", "OK");
            }

            
        }
          void AcceptedEmployeeRequest(string request)
        {

            GetStoreEmployees();
         
        }

        #endregion
    }
}
