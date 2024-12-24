using System.Net.Http.Json;
using Data;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Blazor.Pages.Share
{
    public partial class Employy
    {
        [Inject] HttpClient http {  get; set; }

        [Inject]  NavigationManager _NavigationManager { get; set; }

        private List<Employee> GridData = new List<Employee>();

        private Employee _employee = new Employee();
        private SfGrid<Employee> Grid { get; set; }

        Employee? EmployTodelete;
        bool ShowDeleteDialog = false;

        void DeleteEmploy(Employee employee)
        {
            EmployTodelete = employee;
            ShowDeleteDialog = true;
        }

        void CancelDeleteEmploy()
        {
            ShowDeleteDialog = false;
        }
        public async Task ConfirmDeleteContact()
        {
            ShowDeleteDialog = false;
            var response = await http.DeleteAsync($"api/Employ/{EmployTodelete.Id}");
            if (response.IsSuccessStatusCode)
            {
                GridData = await http.GetFromJsonAsync<List<Employee>>("api/Employ");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            GridData = await http.GetFromJsonAsync<List<Employee>>("api/Employ");
        }

        public async Task HandleDelete(int id)
        {
            var response = await http.DeleteAsync($"api/Employ/{id}");
            if (response.IsSuccessStatusCode)
            {
                GridData = await http.GetFromJsonAsync<List<Employee>>("api/Employ");
            }
        }


        void EditEmploy(int id)
        {
            _NavigationManager.NavigateTo($"Employy/edit/{id}");
        }


    }
}
