namespace SIS.Domain.Model.Entity
{
    using System;
    using System.Collections.Generic;

    using SIS.Domain.Contract;

    /// <summary>
    /// Организация.
    /// </summary>
    public class Organization : IHaveId
    {
        public Organization()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }

        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Наименования организации.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}