﻿using System.ComponentModel.DataAnnotations;

namespace Luna_Edge.Model
{
    public enum TaskStatus { Pending, InProgress, Completed }
    public enum TaskPriority { Low, Medium, High }
    public class UserTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
    }
}
