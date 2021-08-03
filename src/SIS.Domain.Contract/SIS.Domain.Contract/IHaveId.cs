namespace SIS.Domain.Contract
{
    using System;

    public interface IHaveId
    {
        public Guid Id { get; set; }
    }
}