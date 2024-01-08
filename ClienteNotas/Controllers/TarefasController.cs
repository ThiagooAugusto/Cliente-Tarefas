using ClienteNotas.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Reflection.Metadata.Ecma335;
using static Azure.Core.HttpHeader;

namespace ClienteNotas.Controllers
{
    public class TarefasController : Controller
    {
        // GET: Tarefas
        HttpClient client = new HttpClient();

        public TarefasController()
        {
            client.BaseAddress = new Uri("https://apitarefas2024.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ActionResult Index()
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            HttpResponseMessage response = client.GetAsync("/api/Tarefas").Result;
            if (response.IsSuccessStatusCode)
            {
                tarefas = response.Content.ReadFromJsonAsync<List<Tarefa>>().Result;
            }
            return View(tarefas);
        }

        // GET: Tarefas/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/Tarefas/{id}").Result;
            Tarefa tarefa = response.Content.ReadFromJsonAsync<Tarefa>().Result;
            if(tarefa != null)
            {
                return View(tarefa);
            }
            else
            {
                return NotFound();
            }
           
        }

        // GET: Tarefas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarefa tarefa)
        {
            try {
                HttpResponseMessage response = client.PostAsJsonAsync<Tarefa>("api/Tarefas", tarefa).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao criar tarefa";
                    return View();
                }
            }
            catch
            {
                return View();
            }
           
        }

        // GET: Tarefas/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = client.GetAsync($"api/Tarefas/{id}").Result;
            Tarefa tarefa = response.Content.ReadFromJsonAsync<Tarefa>().Result;

            if(tarefa != null)
            {
                return View(tarefa);
            }
            else
            {
                return NotFound();
            }
           
        }

        // POST: TarefasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarefa tarefa)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Tarefa>($"api/Tarefas/{id}", tarefa).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao Editar tarefa";
                    return View();
                }
            }
            catch
            {
                return View();
            }

        }

        // GET: Tarefas/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.GetAsync($"api/Tarefas/{id}").Result;
            Tarefa tarefa = response.Content.ReadFromJsonAsync<Tarefa>().Result;

            if (tarefa != null)
            {
                return View(tarefa);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Tarefas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"api/Tarefas/{id}").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao deletar registro";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
