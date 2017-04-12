namespace TaskManager.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using TaskManager.Models;

    public class TaskItemsController : Controller
    {
        private readonly TaskContext db = new TaskContext();

        // GET: TaskItems/Create
        public ActionResult Create(int? tid)
        {
            if (tid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskListId = Convert.ToInt32(tid);
            var taskLists = this.db.TaskLists.Find(tid);
            if (taskLists == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.TaskListName = taskLists.TaskName;
            this.ViewBag.taskListId = taskListId;

            return this.View();
        }

        // POST: TaskItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            FormCollection form, [Bind(Include = "Id,TaskItemName,Description,IsCompleted")] TaskItem taskItem)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(taskItem);
            }

            taskItem.TaskId = Convert.ToInt32(form.Get("hdnTaskId"));        
            this.db.TaskItems.Add(taskItem);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("TaskItemIndex", new { tid = taskItem.TaskId });
        }

        // GET: TaskItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskItem = await this.db.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return this.HttpNotFound();
            }

            return this.View(taskItem);
        }

        // POST: TaskItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,TaskItemName,Description,TaskId,IsCompleted")] TaskItem taskItem)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(taskItem).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("TaskItemIndex", new { tid = taskItem.Id });
            }

            return this.View(taskItem);
        }

        // GET: TaskItems/tid
        // tid = TaskListId
        public async Task<ActionResult> TaskItemIndex(int? tid)
        {
            if (tid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskListId = Convert.ToInt32(tid);
            var taskLists = this.db.TaskLists.Find(tid);
            if (taskLists == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.TaskListName = taskLists.TaskName;
            this.ViewBag.taskListId = taskListId;
            return this.View(await this.db.TaskItems.Where(t => t.TaskId == taskListId).ToListAsync());
        }

        // GET: TaskItems/tid
        // tid = TaskListId
        public async Task<ActionResult> TaskItemReadonlyIndex(int? tid)
        {
            if (tid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskListId = Convert.ToInt32(tid);
            var taskLists = this.db.TaskLists.Find(tid);
            if (taskLists == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.TaskListName = taskLists.TaskName;
            this.ViewBag.taskListId = taskListId;
            return this.View(await this.db.TaskItems.Where(t => t.TaskId == taskListId).ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}