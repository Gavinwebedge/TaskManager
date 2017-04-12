namespace TaskManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using TaskManager.Models;

    public class TaskListsController : Controller
    {
        private readonly TaskContext db = new TaskContext();

        // GET: TaskLists/Create
        // Show View to create a TaskList
        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: TaskLists/Create
        // Save a new TaskList
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "Id,TaskName,Description")] TaskList taskList)
        {
            var ownerId = new Guid(this.User.Identity.GetUserId());
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            taskList.OwnerId = ownerId;
            this.db.TaskLists.Add(taskList);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        // GET: TaskLists/Edit/5
        // Edit a TaskList specified by id
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskList = await this.db.TaskLists.FindAsync(id);
            if (taskList == null)
            {
                return this.HttpNotFound();
            }

            return
                this.View(
                    new TaskListViewModel
                        {
                            TaskName = taskList.TaskName, 
                            Description = taskList.Description, 
                            Id = taskList.Id, 
                            OwnerId = taskList.OwnerId
                        });
        }

        // POST: TaskLists/Edit/5
        // Save TaskList specified by id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,TaskName,Description,OwnerId")] TaskList taskList)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(taskList).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return
                this.View(
                    new TaskListViewModel
                        {
                            TaskName = taskList.TaskName, 
                            Description = taskList.Description, 
                            Id = taskList.Id, 
                            OwnerId = taskList.OwnerId
                        });
        }

        // GET: TaskLists
        // List all tasklists for this user
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var ownerId = new Guid(this.User.Identity.GetUserId());
            var taskList = await this.db.TaskLists.Where(t => t.OwnerId == ownerId).ToListAsync();
            var taskListView = this.MapTaskList(taskList);
            return this.View(taskListView);
        }

        // GET: TaskLists
        // List all tasklists for all other users
        [Authorize]
        public async Task<ActionResult> OtherIndex()
        {
            var ownerId = new Guid(this.User.Identity.GetUserId());
            var taskList = await this.db.TaskLists.Where(t => t.OwnerId != ownerId).ToListAsync();
            var taskListView = this.MapOtherIndexTaskList(taskList);
            return this.View(taskListView);
        }

        // GET: TaskItemIndex
        // redirect to action in TaskItemsController
        [Authorize]
        public ActionResult TaskItemIndex(int? tid)
        {
            return this.RedirectToAction("TaskItemIndex", "TaskItems", new { tid });
        }

        // GET: TaskItemReadonlyIndex
        // redirect to action in TaskItemsController
        [Authorize]
        public ActionResult TaskItemReadonlyIndex(int? tid)
        {
            return this.RedirectToAction("TaskItemReadonlyIndex", "TaskItems", new { tid });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        private List<OtherIndexViewModel> MapOtherIndexTaskList(IEnumerable<TaskList> taskList)
        {
            var result = new List<OtherIndexViewModel>();
            foreach (var item in taskList)
            {
                var userName =
                    this.HttpContext.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(item.OwnerId.ToString())
                        .UserName;

                var newItem = new OtherIndexViewModel
                                  {
                                      Id = item.Id,
                                      OwnerId = item.OwnerId,
                                      TaskName = item.TaskName,
                                      Description = item.Description,
                                      ListOwner = userName
                                  };
                result.Add(newItem);
            }

            return result;
        }

        private List<TaskListViewModel> MapTaskList(IEnumerable<TaskList> taskList)
        {
            return taskList.Select(item => new TaskListViewModel { TaskName = item.TaskName, Description = item.Description, Id = item.Id, OwnerId = item.OwnerId }).ToList();
        }
    }
}