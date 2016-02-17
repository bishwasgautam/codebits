using System;
using System.Collections.Generic;
using CodeBits.Common;

namespace CodeBits.Entities
{
    public abstract class QuantifiableEntity : ICommentable, IViewAble, IVotable, IFlaggable
    {
        public IEnumerable<Comment> Comments { get; set; }
        public int ViewsCount { get; set; }
        public int VotesCount { get; set; }
        public int CommentsCount { get; set; }

        public bool UpViewCount()
        {
            throw new NotImplementedException();
        }

        public int GetViews()
        {
            throw new NotImplementedException();
        }
        public int Votes { get; set; }

        public bool VoteUp()
        {
            throw new NotImplementedException();
        }

        public bool VoteDown()
        {
            throw new NotImplementedException();
        }

        public int GetVotes()
        {
            throw new NotImplementedException();
        }

        public bool Flag(Flag type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Flag> PendingFlags { get; set; }
        public IEnumerable<Flag> ApprovedFlags { get; set; }
        public bool ForgiveFlag(FlagType type)
        {
            throw new NotImplementedException();
        }
    }

    public class CodeBit : QuantifiableEntity
    {
        public Guid Id { get; set; }
        public string Title{ get; set; }
        public DateTime CreatedTimeStamp{ get; set; }
        public DateTime LastModifiedTimeStamp { get; set; }
       
    }

    public interface IViewAble
    {
        bool UpViewCount();
        int GetViews();
    }

    public interface IVotable
    {
        int Votes { get; set; }
        bool VoteUp();
        bool VoteDown();
        int GetVotes();
    }

    public interface ICommentable
    {
        IEnumerable<Comment> Comments { get; set; }
    }

    public class Tag
    {
        //Always lowercase
        public string Content { get; set; }

        public DateTime CreatedTimeStamp { get; set; }
        public DateTime LastUsedTimeStamp { get; set; }
        public virtual User CreatedBy { get; set; }
        public int UseCount { get; set; }
        //Lazy loading
        public IEnumerable<Tag> RelatedTags { get; set; }

    }

    public class Comment : IVotable, IFlaggable
    {
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public string Body { get; set; }
        
        public virtual User Commentator { get; set; }

        public int Votes { get; set; }

        public bool VoteUp()
        {
            throw new NotImplementedException();
        }

        public bool VoteDown()
        {
            throw new NotImplementedException();
        }

        public int GetVotes()
        {
            throw new NotImplementedException();
        }

        public bool Flag(Flag type)
        {
            throw new NotImplementedException();
        }
        

        public IEnumerable<Flag> PendingFlags { get; set; }
        public IEnumerable<Flag> ApprovedFlags { get; set; }
        public bool ForgiveFlag(FlagType type)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFlaggable
    {
        bool Flag(Flag type);
        IEnumerable<Flag> PendingFlags { get; set; }
        IEnumerable<Flag> ApprovedFlags { get; set; }
        bool ForgiveFlag(FlagType type);

    }

    public enum FlagType
    {
        none,
        inappropriatelanguage,
        hate,
        maliciouscontent,
        plagiarism,
        other

    }

    public class Flag
    {
        public virtual User FlaggedBy { get; set; }
        public FlagType FlagType { get; set; }
        public string Description { get; set; }

       
    }

    public class ShortComment : Comment
    {
        private new string Title { get; set; }
    }

    public class User
    {
        
        public Guid Id{ get; set; }
        public string FirstName{ get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public EmailInfo EmailInfo { get; set; }
        public PhoneInfo PhonesInfo { get; set; }
        public string Handle { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age {
            get {
                return (int) Math.Floor((DateTime.Now - BirthDate).Days / Constants.DateTime.DaysInAYear);
            }
        }
        public Address Address { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public DateTime MemberSince {
            get
            {
                return CreatedDateTime;
            }
        }

        public int ActiveDays
        {
            get
            {
                var ts = DateTime.Now - MemberSince;
                return ts.Days;
            }
        }

        

        public DateTime LastLogin { get; set; }

        //TODO Lazy Loading
        public IEnumerable<User> FriendsNetwork { get; set; }
        public IEnumerable<Tag> AssociatedTags { get; set; }
    }

    public enum AccessLevel
    {
        serviceuser,
        businessuser,
        developer,
        qualitycheck,
        siteadmin
    }

    public class EmailInfo
    {
        public string PrimaryEmail{ get; set; }
        public string SecondaryEmail { get; set; }
    }

    public class PhoneInfo
    {
        public string Home { get; set; }
        public string Cell { get; set; }
        public string Work { get; set; }

        private PreferredNumber _preferred = PreferredNumber.none;

        public PreferredNumber Preferred => _preferred;

        public void SetPreferred(int val)
        {
            try
            {
                _preferred = (PreferredNumber) val;
            }
            catch (Exception)
            {
                _preferred = PreferredNumber.none;
            }

        }

        public void SetPreferred(PreferredNumber preferred)
        {
            _preferred = preferred;
        }

    }

    public enum PreferredNumber
    {
        none = 0,
        home = 1,
        cell = 2,
        work = 3
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class Activity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public virtual User Initiator { get; set; }
        public string Story { get; set; }

        public int ViewsCount { get; set; }
        //TODO <Lazy Loading>
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<CodeBit> CodeBits{ get; set; }

    }

    public class CommentActivity : Activity
    {
        //Underlying comment
        public virtual Comment Comment { get; set; }

        public ActivityType GetType()
        {
            return ActivityType.comment;
        }

      
    }

    public enum ActivityType
    {
        none, codebit, comment
    }

    public class CodeBitActivity : Activity
    {
        //Underlying codebit
        public virtual CodeBit CodeBit { get; set; }

        public ActivityType GetType()
        {
            return ActivityType.codebit;

        }
    }
}

