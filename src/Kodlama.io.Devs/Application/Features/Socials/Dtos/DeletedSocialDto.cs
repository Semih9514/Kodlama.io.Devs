﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Dtos
{
    public class DeletedSocialDto
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Github { get; set; }
    }
}
