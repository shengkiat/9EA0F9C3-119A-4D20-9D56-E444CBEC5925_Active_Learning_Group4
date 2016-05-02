﻿using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;

namespace ActiveLearning.Business.SignalRHub
{
    public class ChatHub : Hub
    {
        public void Send(int courseSid,string studentSid, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(Context.User.Identity.Name, message);
            if(message ==null ||string.IsNullOrEmpty(message))
            {
                message = "";
            }
            using (var chatManager = new ChatManager())
            {
                try
                {
                    string msg = string.Empty;
                    Chat chat = new Chat() { CourseSid = courseSid, CreateDT = DateTime.Now, Message = message, StudentSid = int.Parse(studentSid) };
                    chatManager.AddStudentChatToCourse(chat, chat.StudentSid.Value, chat.CourseSid, out msg);
                }
                catch (Exception ex)
                {
                    BaseManager.ExceptionLog(ex);
                }
            }
            Clients.Group(courseSid.ToString()).addNewMessageToPage(studentSid, Context.User.Identity.Name, message);
        }


        public override Task OnConnected()
       {
            string username = Context.User.Identity.Name;

            var identity = (ClaimsIdentity)Context.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            foreach (var claim in claims)
            {
                if (LiveChatHelper.ParseClaimsType(claim.Type) == "GroupSid")
                {

                   //LiveChatHelper.CourseID = claim.Value;
                    Groups.Add(this.Context.ConnectionId, claim.Value);
                }
            }

            return base.OnConnected();
        }
    }


    public class LiveChatHelper
    {
        public static string ParseClaimsType(string claimType)
        {
            if (claimType == GroupSid)//  "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
            {
                return "GroupSid";
            }
            else
            {
                return string.Empty;
            }
        }

        public static string CourseID { get; set; }

        public static string Anonymous
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous"; }
        }

        public static string Authentication
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication"; }
        }

        public static string AuthorizationDecision
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision"; }
        }

        public static string Country
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country"; }
        }

        public static string DateOfBirth
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth"; }
        }

        public static string DenyOnlySid
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid"; }
        }

        public static string Dns
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns"; }
        }

        public static string Email
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"; }
        }

        public static string Gender
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender"; }
        }

        public static string GivenName
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"; }
        }

        public static string Hash
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash"; }
        }

        public static string HomePhone
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone"; }
        }

        public static string Locality
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"; }
        }

        public static string MobilePhone
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone"; }
        }

        public static string Name
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"; }
        }

        public static string NameIdentifier
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"; }
        }

        public static string OtherPhone
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone"; }
        }

        public static string PostalCode
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode"; }
        }

        public static string PPID
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier"; }
        }

        public static string Rsa
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa"; }
        }

        public static string Sid
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"; }
        }

        public static string Spn
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn"; }
        }

        public static string StateOrProvince
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince"; }
        }

        public static string StreetAddress
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress"; }
        }

        public static string Surname
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"; }
        }

        public static string System
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system"; }
        }

        public static string Thumbprint
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint"; }
        }

        public static string Upn
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn"; }
        }

        public static string Uri
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri"; }
        }

        public static string Webpage
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage"; }
        }

        public static string X500DistinguishedName
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname"; }
        }

        public static string GroupSid
        {
            get { return "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid"; }
        }
    }
}