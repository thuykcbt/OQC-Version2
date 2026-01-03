using DevExpress.XtraEditors.Mask.Design;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Design_Form.Job_Model.Roi_tool;

namespace Design_Form.Job_Model
{
    public class LibaryHalcon
    {
        public HDrawingObject[] Drawobject = new HDrawingObject[100];
        public int i = 0;
        public HDrawingObject[] Drawobject_circle = new HDrawingObject[100];
        public HDrawingObject Drawobject_Line = new HDrawingObject(1000, 1000, 1000, 1000);
        public HDrawingObject[] Drawobject_Polygy = new HDrawingObject[100];
        public void make_ROI_rectang(HWindow hWindow, double XCenter, double YCenter, double Phi, double W, double H, bool mask, int index)
        {

            try
            {
                if (!mask)
                {
                    clear_Obj(hWindow);
                }
                Drawobject[index] = new HDrawingObject();
                Drawobject[index].CreateDrawingObjectRectangle2(XCenter, YCenter, Phi, W, H);

                hWindow.AttachDrawingObjectToWindow(Drawobject[index]);

            }
            catch (HOperatorException ex)
            {
                MessageBox.Show("Error drawing ROI: " + ex.Message);
            }


        }
        public void Make_Roi_Polygon(HWindow hWindow, List<double> row, List<double> colum1,bool mask, int index)
        {
            int index_list = (int)row.Count;
            double[] Rows = new double[index_list];
            double[] Cols = new double[index_list];
            for (int i = 0; i < index_list; i++)
            {
                Rows[i] = row[i];
                Cols[i] = colum1[i];
            }


            try
            {
                if (!mask)
                {
                    clear_Obj(hWindow);
                }
                Drawobject_Polygy[index] = new HDrawingObject();
                Drawobject_Polygy[index].CreateDrawingObjectXld(Rows, Cols);

                hWindow.AttachDrawingObjectToWindow(Drawobject_Polygy[index]);

            }
            catch (HOperatorException ex)
            {
                MessageBox.Show("Error drawing ROI: " + ex.Message);
            }

        }
        public void make_Roi_Line(HWindow hWindow, double row1, double column1, double row2, double column2)
        {
            Drawobject_Line.CreateDrawingObjectLine(row1, column1, row2, column2);
            try
            {


                clear_Obj(hWindow);


                hWindow.AttachDrawingObjectToWindow(Drawobject_Line);

            }
            catch (HOperatorException ex)
            {
                MessageBox.Show("Error drawing ROI: " + ex.Message);
            }
        }
        public void clear_Obj(HWindow hWindow)
        {

            foreach (HDrawingObject obf in Drawobject)
            {
                if (obf != null)

                {
                    hWindow.DetachDrawingObjectFromWindow(obf);
                }
            }
            foreach (HDrawingObject obf in Drawobject_circle)
            {
                if (obf != null)

                {
                    hWindow.DetachDrawingObjectFromWindow(obf);
                }
            }
            foreach (HDrawingObject obf in Drawobject_Polygy)
            {
                if (obf != null)

                {
                    hWindow.DetachDrawingObjectFromWindow(obf);
                }
            }

            if (Drawobject_Line != null)
            {
                hWindow.DetachDrawingObjectFromWindow(Drawobject_Line);
            }

        }
     
        public void make_Roi_Circle(HWindow hWindow, double row, double column, double radius, bool mask, int index)
        {

            try
            {

                if (!mask)
                {
                    clear_Obj(hWindow);
                }
                Drawobject_circle[index] = new HDrawingObject();
                Drawobject_circle[index].CreateDrawingObjectCircle(row, column, radius);

                hWindow.AttachDrawingObjectToWindow(Drawobject_circle[index]);

            }
            catch (HOperatorException ex)
            {
                MessageBox.Show("Error drawing ROI: " + ex.Message);
            }
        }
        public void get_roi_Rectang(HDrawingObject dobj,out double StartX, out double StartY, out double Phi, out double WidthX,out double HeightY)
        {
            string[] pararectange2 = { "column", "row", "phi", "length1", "length2" };
            HTuple paraname = pararectange2;
            HTuple Param = dobj.GetDrawingObjectParams(paraname);

            StartY = Param[0];
            StartX = Param[1];
            Phi = Param[2];
            WidthX = Param[3];
            HeightY = Param[4];
        }
        public void get_roi_Line(HDrawingObject dobj, out double StartX1, out double StartY1,  out double EndX2, out double EndY2)
        {
            string[] pararectange3 = { "row1", "column1", "row2", "column2" };
            HTuple paraname1 = pararectange3;
            HTuple Param1 = dobj.GetDrawingObjectParams(paraname1);
            StartX1 = Param1[0];
            StartY1 = Param1[1];

            EndX2 = Param1[2];
            EndY2 = Param1[3];

        }
        public void get_roi_Circle(HDrawingObject dobj, out double StartX1, out double StartY1, out double Radiuns)
        {
            string[] pararectange3 = { "row", "column", "radius" };
            HTuple paraname1 = pararectange3;
            HTuple Param1 = dobj.GetDrawingObjectParams(paraname1);
            StartX1 = Param1[0];
            StartY1 = Param1[1];
            Radiuns = Param1[2];

        }
        public void get_roi_Pylygon(HDrawingObject dobj,out List<double> row,out List<double> column)
        {
            List<double> rows = new List<double>();
            List<double> columns = new List<double>();
            string[] pararectange3 = { "row", "column" };
            HTuple paraname1 = pararectange3;
            HTuple Param1 = dobj.GetDrawingObjectParams(paraname1);
            for (int i = 0; i < Param1.Length / 2; i++)
            {
                rows.Add((double)Param1[i]); 

            }
            for (int i = Param1.Length / 2; i < Param1.Length; i++)
            {
               columns.Add((double)Param1[i]);
            }
            row = rows; column = columns;
            
        }
        public void Align_Roi_Line(HTuple homMat2D, double x1, double y1, double x2, double y2, out HTuple x1_align, out HTuple y1_align, out HTuple x2_align, out HTuple y2_align)
        {
            HOperatorSet.AffineTransPoint2d(homMat2D, (HTuple)x1, (HTuple)y1, out x1_align, out y1_align);
            HOperatorSet.AffineTransPoint2d(homMat2D, (HTuple)x2, (HTuple)y2, out x2_align, out y2_align);
        }
       
        public void Align_Tool_Cir(HTuple homMat2D, double x1, double y1,double phi, out HObject ho_ImageROI)
        {
            HTuple x1_align, y1_align; 
            HOperatorSet.AffineTransPoint2d(homMat2D, (HTuple)x1, (HTuple)y1, out x1_align, out y1_align);
            HOperatorSet.GenCircle(out ho_ImageROI, x1_align, y1_align, phi);
        }
        public void Alingn_Tool_Rectang(HTuple homMat2D, double x1, double y1,double phi,double width,double height, out HObject region_Rec_align)
        {
            HObject ho_ImageROI;
            HOperatorSet.GenRectangle2(out ho_ImageROI, x1, y1,phi, width, height);
            HOperatorSet.AffineTransRegion(ho_ImageROI, out region_Rec_align, homMat2D, "nearest_neighbor");

        }
        public void Align_Tool_Line (HTuple homMat2D, double x1, double y1, double x2, double y2, out HObject ho_RoiLine)
        {
            HObject ho_ImageROI;
            HOperatorSet.GenEmptyObj(out ho_ImageROI);
            HOperatorSet.GenEmptyObj(out ho_RoiLine);
            HOperatorSet.GenRegionLine(out ho_ImageROI, x1, y1, x2, y2);
            HOperatorSet.AffineTransRegion(ho_ImageROI, out ho_RoiLine, homMat2D, "nearest_neighbor");
        }
        public void Align_Tool_Polygon(HTuple homMat2D,List<double> Row, List<double> Col, out HObject ho_RoiPolygon)
        {
            HObject ho_ImageROI;
            HOperatorSet.GenEmptyObj(out ho_ImageROI);
            HOperatorSet.GenEmptyObj(out ho_RoiPolygon);
            int index_list = (int)Row.Count;
            double[] Rows = new double[index_list];
            double[] Cols = new double[index_list];
            for (int i = 0; i < index_list; i++)
            {
                Rows[i] = Row[i];
                Cols[i] = Col[i];
            }
            HOperatorSet.GenRegionPolygon(out ho_ImageROI, Rows, Cols);
            HOperatorSet.AffineTransRegion(ho_ImageROI, out ho_RoiPolygon, homMat2D, "nearest_neighbor");
        }
       
    }
}
