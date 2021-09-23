﻿using eProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}
