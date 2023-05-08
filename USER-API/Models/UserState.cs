﻿using System;
using System.Collections.Generic;

namespace USER_API.Models;

public partial class UserState : BaseModel
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
