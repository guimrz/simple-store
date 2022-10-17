﻿namespace SimpleStore.Services.Catalog.Objects.Responses
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
