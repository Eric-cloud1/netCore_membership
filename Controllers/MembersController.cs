using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberApi.Data;
using MemberApi.Models;
using MemberApi.Models.Members;
using MemberApi.Models.Services;

namespace MemberApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MembersController : Controller
    {
        public IList<Contact> Contact { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        public MembersController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }


        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            // var applicationDbContext = _context.ProductLot.Include(p => p.RegulatorySystem).Include(p => p.Dispensary).Include(p => p.Product).Include(p => p.TestResult);
            var contacts = from c in _context.Contact
                           select c;

            foreach (Contact c in contacts)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(c.Email);
                    if (user is null)
                        continue;
        
                var roles = await _userManager.GetRolesAsync(user);
                foreach (string role in roles)
                {
                    if (role != ContactStatus.Registered.ToString() &&
                        role != ContactStatus.Approved.ToString() &&
                        role != ContactStatus.Lockout.ToString())
                            c.Role = role;
                }

                }
                catch { continue; }

            }


            return View(await contacts.ToListAsync());
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Contact);
        }


        public async Task<IActionResult> Post(int id, ContactStatus status)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(c => c.ContactId == id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.Status = status;
            _context.Contact.Update(contact);

            //todo: update role

            var registeredUser = new ApplicationUser();
            registeredUser = await _userManager.FindByEmailAsync(contact.Email);

            if (registeredUser != null)
            {
                var statusRoles = await _userManager.GetRolesAsync(registeredUser);
                foreach (string role in statusRoles)
                {
                    if (role == ContactStatus.Registered.ToString() ||
                        role == ContactStatus.Approved.ToString() ||
                        role == ContactStatus.Lockout.ToString())
                        await _userManager.RemoveFromRoleAsync(registeredUser, role);
                }

                await _userManager.AddToRoleAsync(registeredUser, contact.Status.ToString());
            }
                await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(c => c.ContactId == id);


            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(contact.Email);
            if (user is null)
                return NotFound();

            var rolesList = new[] {
                new Roles { Id = "GoldMember", Name = "GoldMember" },
                new Roles { Id = "SilverMember", Name = "SilverMember" },
                new Roles { Id = "BronzeSilver", Name = "BronzeSilver" },
                new Roles { Id = "Admin", Name = "Admin" }
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                if (role != ContactStatus.Registered.ToString() &&
                    role != ContactStatus.Approved.ToString() &&
                    role != ContactStatus.Lockout.ToString())
                    contact.Role = role;
            }

            

            var selectList = new SelectList(rolesList, "Id", "Name", contact.Role);
            ViewData["Roles"] = selectList;

            return View(contact);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,Role,Address,City,State,Zip,Email")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    var registeredUser = new ApplicationUser();
                    registeredUser = await _userManager.FindByEmailAsync(contact.Email);

                    if (registeredUser != null)
                    {
                        var OldRoles = await _userManager.GetRolesAsync(registeredUser);
                      
                        foreach (string role in OldRoles)
                        {
                            if (role != ContactStatus.Registered.ToString() &&
                                role != ContactStatus.Approved.ToString() &&
                                role != ContactStatus.Lockout.ToString())
                                await _userManager.RemoveFromRoleAsync(registeredUser, role);
                        }
                        var selectedRole = Request.Form["RolesList"];
                        
                        await _userManager.AddToRoleAsync(registeredUser, selectedRole);

                        contact.Role = selectedRole;
                        _context.Update(contact);
                        await _context.SaveChangesAsync();

                    }
                    else { return NotFound();}
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByEmailAsync(contact.Email);
            if (user is null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                if (role != ContactStatus.Registered.ToString() &&
                    role != ContactStatus.Approved.ToString() &&
                    role != ContactStatus.Lockout.ToString())
                     contact.Role = role;
            }

           
            return View(contact);
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .SingleOrDefaultAsync(c => c.ContactId == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(c => c.ContactId == id);

            var user = await _userManager.FindByEmailAsync(contact.Email);
            if (user != null)
            {
                var _roles = await _userManager.GetRolesAsync(user);
                foreach (string role in _roles)
                    await _userManager.RemoveFromRoleAsync(user, role);

                await _userManager.DeleteAsync(user);
            }
            
            _context.Contact.Remove(contact);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }

    }
}