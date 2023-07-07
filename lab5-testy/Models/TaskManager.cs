namespace lab5_testy.Models
{
    public class TaskManager
    {
        private readonly TaskDbContext _dbContext;
        public TaskManager(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Task> GetTasks()
        {
            return _dbContext.Tasks.ToList();
        }
        public Task GetTaskById(int id)
        {
            return _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        }
        public void AddTask(Task task)
        {
            task.Id = GenerateNewTaskId();
            _dbContext.Add(task);
            _dbContext.SaveChanges();
        }
        public bool UpdateTask(int id, Task updatedTask)
        {
            var existingTask = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.IsCompleted = updatedTask.IsCompleted;
                return true;
            }
            return false;
        }
        public bool MarkTaskAsCompleted(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                _dbContext.SaveChanges();
                return true;
            }
            _dbContext.SaveChanges();
            return false;
        }
        public bool DeleteTask(int id)
        {
            var taskToRemove = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                _dbContext.Remove(taskToRemove);
                return true;
            }
            return false;
        }
        private int GenerateNewTaskId()
        {
            if (_dbContext.Tasks.Any())
            {
                return _dbContext.Tasks.Max(t => t.Id) + 1;
            }
            return 1;
        }
        public bool RemoveTask(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                return true;
            }
            _dbContext.SaveChanges();
            return false;
        }
    }

}
