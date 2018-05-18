﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiNetCoreDemo.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HiNetCoreDemo.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly HiNetCoreDemo.Data.HiNetCoreDbContext _context;

        public IndexModel(HiNetCoreDemo.Data.HiNetCoreDbContext context)
        {
            _context = context;
        }
        public List<HiNetCoreDemo.Models.Student> Students { get; set; }
        public List<HiNetCoreDemo.Models.Subject> Subjects { get; set; }

        [TempData]
        public string NameSortParm { get; set; }        
        [TempData]
        public string IdSortParm { get; set; }

        [TempData]
        public string CurrentFilter { get; set; }
        public async Task<IActionResult> OnGet(string sortOrder,string currentFilter)
        {
            //Students = _context.Student.Include(m => m.Subjects).OrderBy(x => x.StId).ToList();

            TempData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            TempData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            TempData["CurrentFilter"] = currentFilter;

            var students = from m in _context.Student.Include(m => m.Subjects)
                           select m;
            if (!string.IsNullOrEmpty(currentFilter))
            {
                students = students.Where(m => m.StId.Contains(currentFilter) || m.StName.Contains(currentFilter));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(x => x.StName);
                    break;
                case "Name":
                    students = students.OrderBy(x => x.StName);
                    break;
                case "id_desc":
                    students = students.OrderByDescending(x => x.StId);
                    break;
                default:
                    students = students.OrderBy(x => x.StId);
                    break;
            }
            Students = await students.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostFilterAsync(string sortOrder, string searchString)
        {

            TempData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            TempData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            TempData["CurrentFilter"] = searchString;

            var students = from m in _context.Student.Include(x => x.Subjects).OrderBy(x => x.StId)
                           select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(m => m.StId.Contains(searchString) || m.StName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(x => x.StName);
                    break;
                case "Name":
                    students = students.OrderBy(x => x.StName);
                    break;
                case "id_desc":
                    students = students.OrderByDescending(x => x.StId);
                    break;
                default:
                    students = students.OrderBy(x => x.StId);
                    break;
            }

            Students = await students.ToListAsync();
            return Page();
        }

    }
}