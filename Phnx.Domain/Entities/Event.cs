using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{

    public class Event : BaseDeletableEntity
    {
        private Event() { }
        private Event(string name, string description, EventType eventType, DateTime date, List<string> imageUrls)
        {
            Name = name;
            Description = description;
            EventType = eventType;
            Date = date.ToUniversalTime();
            ImageUrls = imageUrls;
        }

        public static Event Create(string name, string description, EventType eventType, DateTime date, List<string> imageUrls) =>
            new(name, description, eventType, date, imageUrls);

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public EventType EventType { get; private set; }
        public DateTime Date { get; private set; }
        public List<string> ImageUrls { get; private set; } = [];

        public void Update(string name, string description, EventType eventType, DateTime date, List<string> imageUrls)
        {
            Name = name;
            Description = description;
            EventType = eventType;
            Date = date.ToUniversalTime();
            ImageUrls = imageUrls;
        }
    }
}