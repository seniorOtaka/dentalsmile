﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using System.Text.RegularExpressions;
using smileUp.DataModel;

namespace smileUp
{
    public class JawVisual3D : SmileVisual3D
    {
        public Dictionary<String, GumVisual3D> gums = new Dictionary<String, GumVisual3D>();
        public GumVisual3D selectedGum;
        public Patient patient;

        public GumContainer gc;
        public ToothContainer tc;
        public BraceContainer bc;
        public WireContainer wc;
        public MeasurementContainer mc;

        public JawVisual3D(Patient p)
        {
            //sample();
            this.patient = p;

            gc = new GumContainer();
            tc = new ToothContainer();
            bc = new BraceContainer();
            wc = new WireContainer();
            mc = new MeasurementContainer();

            displayGumContainer(true);
            displayTeethContainer(true);
            displayBraceContainer(true);
            displayWireContainer(true);
            displayMeasurementContainer(true);
        }

        public void displayMeasurementContainer(bool b)
        {
            try
            {
                if (b)
                    this.Children.Add(mc);
                else
                    this.Children.Remove(mc);
            }
            catch (Exception e) { }
        }

        public void displayBraceContainer(bool b)
        {
            try
            {
                if (b)
                    this.Children.Add(bc);
                else
                    this.Children.Remove(bc);
                }catch (Exception e){ }
        }

        public void displayGumContainer(bool b)
        {
            try{
                if (b)
                    this.Children.Add(gc);
                else
                    this.Children.Remove(gc);
            }catch (Exception e) { }
        }

        public void displayTeethContainer(bool b)
        {
            try{
                if (b)
                    this.Children.Add(tc);
                else
                    this.Children.Remove(tc);
                
                if (b)
                    this.Children.Add(mc);
                else
                    this.Children.Remove(mc);
            }
            catch (Exception e) { }
        }

        public void displayWireContainer(bool b)
        {
            try{
                if (b)
                    this.Children.Add(wc);
                else
                    this.Children.Remove(wc);
            }
            catch (Exception e) { }
        }


        void sample()
        {
            /*

            TeethVisual3D t = new TeethVisual3D(Colors.Green, this);
            this.Children.Add(t);
            TeethVisual3D t2 = new TeethVisual3D(Colors.Red, this);
            this.Children.Add(t2);
            TeethVisual3D t3 = new TeethVisual3D(Colors.Blue, this);
            this.Children.Add(t3);
            TeethVisual3D t4 = new TeethVisual3D(Colors.Yellow, this);
            this.Children.Add(t4);
            TeethVisual3D t5 = new TeethVisual3D(Colors.SkyBlue, this);
            this.Children.Add(t5);
            TeethVisual3D t6 = new TeethVisual3D(Colors.White, this);
            this.Children.Add(t6);
            TeethVisual3D t7 = new TeethVisual3D(Colors.Azure, this);
            this.Children.Add(t7);
            TeethVisual3D t8 = new TeethVisual3D(Colors.Pink, this);
            this.Children.Add(t8);
            TeethVisual3D t9 = new TeethVisual3D(Colors.Blue, this);
            this.Children.Add(t9);
            
            */

            //GumVisual3D gum1 = new GumVisual3D();
            //this.Children.Add(gum1);
            //gums.Add("gumDefault", gum1);
        }

        //Add new GUM to GumContainer
        public GumVisual3D addNewGum(string id)
        {
            GumVisual3D gum = null;
            gum = new GumVisual3D(this,id);
            gums.Add(gum.Id, gum);
            selectedGum = gum;
            gc.Children.Add(gum);
            return gum;
        }

        public TeethVisual3D addTeeth(Point3D center)
        {
            TeethVisual3D teeth = null;
            if (selectedGum != null)
            {
                selectedGum.selectedPoint = center;
                //return selectedGum.addTeeth();
                teeth = selectedGum.addNewTeeth();
            }

            return teeth;            
        }

        public void removeTeeth()
        {
            if (selectedGum != null)
            {
                //selectedGum.removeTeeth();
                selectedGum.deleteTeeth();
            }
        }
        
        //Using Container
        internal TeethVisual3D findTeeth(int p)
        {
            TeethVisual3D teeth = null;
            String pStr = p.ToString("00");
            foreach (var t in tc.Children)
            {
                if (t is TeethVisual3D)
                {
                    teeth = (TeethVisual3D)t;
                    if (Regex.IsMatch(teeth.Id, @"teeth" + pStr))
                    {
                        //teeth.showHideManipulator();
                        selectedGum = teeth.Parent;
                        selectedGum.selectedTeeth = teeth;
                        break;
                    }
                }
            }
            return teeth;
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="findTeeth"/>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal TeethVisual3D selectTeeth(int p)
        {
            TeethVisual3D teeth = null;
            String pStr = p.ToString("00");
            foreach (var g in gums)
            {
                GumVisual3D gum = g.Value;
                if (gum != null)
                {
                    foreach (var t in gum.Children)
                    {
                        if (t is TeethVisual3D)
                        {
                            teeth = (TeethVisual3D)t;
                            //Console.WriteLine(teeth.Id);
                            if(Regex.IsMatch(teeth.Id, @"teeth" + pStr))
                            //if (teeth.Id.StartsWith("teeth" + pStr))
                            {
                                //teeth.showHideManipulator();
                                selectedGum = gum;
                                gum.selectedTeeth = teeth;
                                break;
                            }
                        }
                    }
                }
            }
            return teeth;
        }

        internal BraceVisual3D addBrace(Point3D center)
        {
            BraceVisual3D brace = null;

            if (selectedGum != null)
            {
                selectedGum.selectedPoint = center;
                //return selectedGum.addBrace();
                brace = selectedGum.addNewBrace();
            }
            return brace;
        }

        internal void removeBrace()
        {
            if (selectedGum != null)
            {
                //selectedGum.removeBrace();
                selectedGum.deleteBrace();
            }
        }

        internal void addTeeth(List<GeometryModel3D> models)
        {
            selectOrAddGum();
            foreach (var model in models)
            {
                //selectedGum.addTeeth(model);
                selectedGum.addNewTeeth(model);
            }
        }

        private void selectOrAddGum()
        {
            if (selectedGum == null)
            {
                if (gums.Count == 0)
                {
                    addNewGum(null);
                }
            }
        }

        internal void replaceGum(GeometryModel3D gumModel)
        {
            selectOrAddGum();
            selectedGum.Content = gumModel;
        }

        internal void drawWire(List<BraceVisual3D> bracesModel)
        {
            //clearWires();
            for (var i = 1; i < bracesModel.Count; i++)
            {
                BraceVisual3D brace1 = bracesModel[i - 1];
                BraceVisual3D brace2 = bracesModel[i];

                //brace1.SetMesh(brace1.ToWorldMesh());
                //brace2.SetMesh(brace2.ToWorldMesh());

                //brace1.SetMesh(brace1.ToLocalMesh(brace1.GetMesh()));
                //brace2.SetMesh(brace1.ToLocalMesh(brace2.GetMesh()));

                WireVisual3D wv = new WireVisual3D((i % 2 == 0? Brushes.Green: Brushes.Blue), brace1, brace2);
                //this.Children.Add(wv);
                wc.Children.Add(wv);
            }
        }

        private void clearWires()
        {
            //List<Visual3D> list = this.Children.ToList();
            List<Visual3D> list = wc.Children.ToList();
            foreach (var t in list)
            {
                if (t is WireVisual3D)
                {
                    //this.Children.Remove(t);
                    wc.Children.Remove(t);
                }
            }
        }

        internal void removeWire(BraceVisual3D brace)
        {
            //List<Visual3D> list = this.Children.ToList();
            List<Visual3D> list = wc.Children.ToList();
            foreach (var t in list)
            {
                if (t is WireVisual3D)
                {
                    WireVisual3D wv = (WireVisual3D)t;
                    if (brace.Id == wv.Brace1.Id)
                    {
                        //this.Children.Remove(t);
                        wc.Children.Remove(t);
                    }
                    else if (brace.Id == wv.Brace2.Id)
                    {
                        //this.Children.Remove(t);
                        wc.Children.Remove(t);
                    }
                }
            }
        
        }

        internal void updateTeethMap(string oldid, string newid)
        {
            //find the existing new id
            foreach (var t in tc.Children)
            {
                if (t is TeethVisual3D)
                {
                    TeethVisual3D teeth = (TeethVisual3D)t;
                    if (teeth.Id.Equals(newid))
                    {
                        teeth.Id = oldid;
                        selectedGum.selectedTeeth.Id = newid;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// @Deprecated
        /// <see cref="updateTeethMap"/>
        /// </summary>
        /// <param name="oldid"></param>
        /// <param name="newid"></param>
        internal void updateTeethMapOld(string oldid, string newid)
        {
            //find the existing new id
            foreach (var g in gums)
            {
                GumVisual3D gum = g.Value;
                if (gum != null)
                {
                    foreach (var t in gum.Children)
                    {
                        if (t is TeethVisual3D)
                        {
                            TeethVisual3D teeth = (TeethVisual3D)t;
                            if (teeth.Id.Equals(newid))
                            {
                                teeth.Id = oldid;
                                selectedGum.selectedTeeth.Id = newid;
                                break;
                            }
                        }
                    }
                }
            }
        }

        internal void drawWires()
        {
            if (selectedGum != null && selectedGum.braces != null) drawWire(selectedGum.braces);
        }
		
		//added by achie
        //coba line
        public static double EarthRadius = 0; // km
        public static double DefaultCruisingAltitude = 300; // km  // real value is 12.5...
        public static double DefaultTakeoffLength = 200; // km   // real value is much less..
        public static double DefaultCruisingSpeed = 890; // km/h

        public double CruisingSpeed { get; set; }
        public double Distance { get; set; }

        public JawVisual3D(Point3D p1, Point3D p2)
        {

            LineGeometry myLineGeometry = new LineGeometry();
            var tube = new TubeVisual3D();
            tube.Material = Materials.Blue;
            tube.Fill = new SolidColorBrush(Color.FromArgb(80, 0, 0, 0));
            Children.Add(tube);
            Children.Add(new SphereVisual3D() { Center = p1, Radius = 0.07, Material = Materials.Red });
            Children.Add(new SphereVisual3D() { Center = p2, Radius = 0.07, Material = Materials.Green });


            //Point3DCollection pts = new Point3DCollection();


            //pts.Add(p1);
            //pts.Add(p2);

            //LinesVisual3D ln = new LinesVisual3D();
            //ln.Points = pts;
            //ln.Color = Colors.Black;
            //Children.Add(ln);

            //added by achie
            CruisingSpeed = DefaultCruisingSpeed;
            double cruisingRadius = EarthRadius + DefaultCruisingAltitude;
            double takeoffLength = DefaultTakeoffLength;
            double groundRadius = EarthRadius;

            var o = new Point3D(0, 0, 0);
            var v1 = p1 - o;
            var v2 = p2 - o;
            var z = Vector3D.CrossProduct(v1, v2);
            var x = v1;
            var y = Vector3D.CrossProduct(x, z);
            x.Normalize();
            y.Normalize();
            double v2X = Vector3D.DotProduct(v2, x);
            double v2Y = Vector3D.DotProduct(v2, y);
            double v2A = Math.Atan2(v2Y, v2X);

            const int n = 100;

            var pts = new Point3DCollection();

            double da = v2A / (n - 1);

            double distance = cruisingRadius * Math.Abs(v2A);
            double landingLength = takeoffLength;

            double l = 0;
            for (int i = 0; i < n; i++)
            {
                double a = i * da;
                Vector3D v = x * Math.Cos(a) + y * Math.Sin(a);
                double r = cruisingRadius;

                r = groundRadius + Math.Sin(Math.PI * i / (n - 1)) * (cruisingRadius - groundRadius);

                var p = o + v * r;
                //  Children.Add(new SphereVisual3D() { Center = p, Radius = 60, Material = Materials.Gray});

                pts.Add(p);
                l += Math.Abs(cruisingRadius * da);
            }

            Distance = distance;
        }



        public void updateBraceLocation(string braceid, int oldLocation, int newValue)
        {
            BraceVisual3D brace = findBrace(braceid);
            if (brace != null)
            {
                brace.IsOuterBrace = brace.IntToBoolOuterBrace(newValue);
            }
        }

        private BraceVisual3D findBrace(string braceid)
        {
            //find the existing new id
            foreach (var t in bc.Children)
            {
                if (t is BraceVisual3D)
                {
                    BraceVisual3D brace = (BraceVisual3D)t;
                    if (brace.Id.Equals(braceid))
                    {
                        return brace;
                    }
                }
            }
            return null;
        }


        internal void displayArchs(bool p)
        {
            if(selectedGum != null)
                selectedGum.displayArchs(p);
        }
    }
}
