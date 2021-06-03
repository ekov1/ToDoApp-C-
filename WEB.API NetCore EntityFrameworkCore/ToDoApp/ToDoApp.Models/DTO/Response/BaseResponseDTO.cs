using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models.DTO.Response
{
    public class BaseResponseDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
