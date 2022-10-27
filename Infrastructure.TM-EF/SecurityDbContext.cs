﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TM_EF;
public class SecurityDbContext : IdentityDbContext
{
	public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
	{

	}

}
