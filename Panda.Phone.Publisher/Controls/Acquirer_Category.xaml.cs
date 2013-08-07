using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Panda.Phone.Publisher.ViewModel;
using Panda.Phone.Acquirer;

namespace Panda.Phone.Publisher.Controls
{
    public partial class Acquirer_Category : UserControl
    {
        public Acquirer_Category()
        {
            InitializeComponent();
            
        }

        private void tile_Ewaste_Tap(object sender, GestureEventArgs e)
        {
            if (tile_Ewaste.img_Allready.Opacity == 0)
            {
                tile_Ewaste.img_Allready.Opacity = 1;
            //    ((AcquirerPage)((Grid)this.Parent).Parent).category[0] = true;
                AcquirerVM.category[0] = true;
                VolunteerVM.category[0] = true;

            }
            else
            {
                tile_Ewaste.img_Allready.Opacity = 0;
               // ((AcquirerPage)((Grid)this.Parent).Parent).category[0] = false;
                AcquirerVM.category[0] = false;
                VolunteerVM.category[0] = false;
            }
            
        }

       

        private void tile_Plastic_Tap(object sender, GestureEventArgs e)
        {
            if (tile_Plastic.img_Allready.Opacity == 0)
            {
                tile_Plastic.img_Allready.Opacity = 1;
                AcquirerVM.category[5] = true;
                VolunteerVM.category[5] = true;
               // ((AcquirerPage)((Grid)this.Parent).Parent).category[5] = true;
            }
            else
            {
                tile_Plastic.img_Allready.Opacity = 0;
                AcquirerVM.category[5] = false;
                VolunteerVM.category[5] = false;
                //((AcquirerPage)((Grid)this.Parent).Parent).category[5] = false;
            }
            
        }

        private void tile_Paper_Tap(object sender, GestureEventArgs e)
        {
            if (tile_Paper.img_Allready.Opacity == 0)
            {
                tile_Paper.img_Allready.Opacity = 1;
                AcquirerVM.category[4] = true;
                VolunteerVM.category[4] = true;
                //((AcquirerPage)((Grid)this.Parent).Parent).category[4] = true;
            }
            else
            {
                tile_Paper.img_Allready.Opacity = 0;
                AcquirerVM.category[4] = false;
                VolunteerVM.category[4] = false;
                //((AcquirerPage)((Grid)this.Parent).Parent).category[4] = false;
            }
            
        }

        private void tile_Metal_Tap(object sender, GestureEventArgs e)
        {
            if (tile_Metal.img_Allready.Opacity == 0)
            {
                tile_Metal.img_Allready.Opacity = 1;
                AcquirerVM.category[3] = true;
                VolunteerVM.category[3] = true;
               // ((AcquirerPage)((Grid)this.Parent).Parent).category[3] = true;
            }
            else
            {
                tile_Metal.img_Allready.Opacity = 0;
                AcquirerVM.category[3] = false;
                VolunteerVM.category[3] = false;
              //  ((AcquirerPage)((Grid)this.Parent).Parent).category[3] = false;
            }
            
        }

        private void tile_Fabric_Tap(object sender, GestureEventArgs e)
        {
            if(tile_Fabric.img_Allready.Opacity ==0)
            {
            tile_Fabric.img_Allready.Opacity = 1;
            AcquirerVM.category[1] = true;
            VolunteerVM.category[1] = true;
           // ((AcquirerPage)((Grid)this.Parent).Parent).category[1] = true;
            }else
            {
                tile_Fabric.img_Allready.Opacity = 0;
                AcquirerVM.category[1] = false;
                VolunteerVM.category[1] = false;
              //  ((AcquirerPage)((Grid)this.Parent).Parent).category[1] = false;
            }
            
        }

        private void tile_Glass_Tap(object sender, GestureEventArgs e)
        {
            if (tile_Glass.img_Allready.Opacity == 0)
            {
                tile_Glass.img_Allready.Opacity = 1;
                AcquirerVM.category[2] = true;
                VolunteerVM.category[2] = true;
               // ((AcquirerPage)((Grid)this.Parent).Parent).category[2] = true;
            }
            else
            {
                tile_Glass.img_Allready.Opacity = 0;
                AcquirerVM.category[2] = false;
                VolunteerVM.category[2] = false;
                //((AcquirerPage)((Grid)this.Parent).Parent).category[2] = false;
            }
            
        }

        
    }
}
