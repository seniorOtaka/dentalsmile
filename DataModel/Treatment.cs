﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Treatment
    {
        public string Id { get; set; }
        public Phase Phase { get; set; }
        public Dentist Dentist { get; set; }
        public Patient Patient { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentTime { get; set; }
        public string Room { get; set; }
        
        public List<SmileFile> Files { get; set; }
        public string TRefId { get; set; }
    }
}