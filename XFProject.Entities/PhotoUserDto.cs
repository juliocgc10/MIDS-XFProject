using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XFProject.Entities
{
    public class PhotoUserDto : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Fields
        private int photoUserId;
        private string photoName;
        private string photoTitle;
        private string nickNameAutor;
        private string photoDescription;
        private string latitude;
        private string longitude;
        private Guid photoID;
        private string pathUrl;
        private string email;
        #endregion

        #region Properties
        public string PhotoName
        {
            get => photoName;
            set
            {
                photoName = value;
                OnPropertyChanged();
            }
        }

        public string PhotoTitle
        {
            get => photoTitle;
            set
            {
                photoTitle = value;
                OnPropertyChanged();
            }
        }

        public string NickNameAutor
        {
            get => nickNameAutor;
            set
            {
                nickNameAutor = value;
                OnPropertyChanged();
            }
        }

        public string PhotoDescription
        {
            get => photoDescription;
            set
            {
                photoDescription = value;
                OnPropertyChanged();
            }
        }

        public string Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        public string Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        public Guid PhotoID
        {
            get => photoID;
            set
            {
                photoID = value;
                OnPropertyChanged();
            }
        }

        public int PhotoUserId
        {
            get => photoUserId;
            set
            {
                photoUserId = value;
                OnPropertyChanged();
            }
        }

        public string PathUrl
        {
            get => pathUrl;
            set
            {
                pathUrl = value;
                OnPropertyChanged();
            }
        }


        public string PhotoUrlComplete
        {
            get => $"{pathUrl}{PhotoUserId}.jpg";
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string photoBase64;

        public string PhotoBase64
        {
            get { return photoBase64; }
            set { photoBase64 = value; }
        }


        #endregion


        #region Constructors

        public PhotoUserDto()
        {
        }

        #endregion

        #region Methods   

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
