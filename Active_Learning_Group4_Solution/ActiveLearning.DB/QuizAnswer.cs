//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ActiveLearning.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuizAnswer
    {
        public int Sid { get; set; }
        public int QuizQuestionSid { get; set; }
        public int QuizOptionSid { get; set; }
        public int StudentSid { get; set; }
        public System.DateTime CreateDT { get; set; }
        public Nullable<System.DateTime> UpdateDT { get; set; }
        public Nullable<System.DateTime> DeleteDT { get; set; }
    
        public virtual QuizOption QuizOption { get; set; }
        public virtual Student Student { get; set; }
    }
}
