using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Db.DataClasses
{
    public class MobaDbLink
    {
        public int LinkNid { get; set; }
        private Uri _url = new Uri("http://www.dummy.org");
        public string Url
        {
            get
            {
                return _url.AbsoluteUri;
            }
            set
            {
                try
                {
                    if (!value.Contains(Uri.SchemeDelimiter)) value = String.Concat(Uri.UriSchemeHttp, Uri.SchemeDelimiter, value);
                    _url = new Uri(value);
                }
                catch (Exception ex)
                {
                    ex.ToString(); // foo TODO
                }
            }
        }


        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        private Byte[] _bild = null;

        public MobaDbLink()
        {
            LinkNid = -1;
            Bezeichnung = string.Empty;
            Beschreibung = string.Empty;
        }

        public MobaDbLink(int linkId)
            : this()
        {
            LinkNid = linkId;
        }

        public MobaDbLink(string url)
            : this()
        {
            Url = url;
            Bezeichnung = Url;
        }
        public MobaDbLink(string url, string bezeichnung = "", string beschreibung = "")
            : this()
        {
            Url = url;
            Bezeichnung = bezeichnung == string.Empty ? Url : bezeichnung;
            Beschreibung = beschreibung;
        }


        public MobaDbLink(int linkId, string bezeichnung)
            : this(linkId)
        {
            Bezeichnung = bezeichnung;
        }

        public MobaDbLink(int linkId, string bezeichnung, string beschreibung)
            : this(linkId, bezeichnung)
        {
            Beschreibung = beschreibung;
        }

        public void AddPicture(byte[] bild)
        {
            _bild = bild;
        }




        //public void SerializeAndSave() 
        //     { 
        //       try { 
        //        // instantiate a MemoryStream and a new instance of our class          
        //        MemoryStream ms = new MemoryStream(); 
        //        ClassToSerialize c=new ClassToSerialize(txtName.Text); 
        //          // create a new BinaryFormatter instance 
        //          BinaryFormatter b=new BinaryFormatter(); 
        //         // serialize the class into the MemoryStream 
        //          b.Serialize(ms,c); 
        //          ms.Seek(0,0); 
        //         // Show the information 
        //          textBox1.Text="Ms Length: " + ms.Length.ToString(); 
        //          int res=SaveToDB(txtName.Text,ms.ToArray()); 
        //          textBox1.Text+="\nDB RetVal: "+res.ToString() + "\n"; 
        //          //Clean up 
        //          ms.Close(); 
        //         } 
        //          catch(Exception ex) 
        //         { 
        //           textBox1.Text=ex.Message; 
        //         } 
        //     } 
        //public void RetrieveAndDeserialize() 
        //  { 
        //   MemoryStream ms2 = new MemoryStream();
        //   byte[] buf = RetrieveFromDB(txtName.Text); 
        //   ms2.Write ( buf,0,buf.Length );
        //   ms2.Seek(0,0); 
        //   BinaryFormatter b=new BinaryFormatter(); 
        //   ClassToSerialize c=(ClassToSerialize)b.Deserialize(ms2);    
        //  textBox1.Text+="Deserialized Name: " +c.name + "\n"; 
        //   textBox1.Text+="Portion of Deserialized float array: \n"; 
        //   for(int j =0;j<100;j++) 
        //   { 
        //    textBox1.Text+=c.fltArray[j].ToString() +"\n";
        //   } 
        //   ms2.Close(); 
        //  } 
        // private int SaveToDB(string imgName, byte[] imgbin 
        //  { 
        //   SqlConnection connection = new SqlConnection("Server=(local);DataBase=Northwind;User Id=sa;Password=;");

        //   SqlCommand command = new SqlCommand( "INSERT INTO Employees (firstname,lastname,photo)
        //        VALUES  (@img_name, @img_name, @img_data )", connection ); 
        //   // (need to write something to first and lastname columns 
        //   // because of constraints) 
        //  SqlParameter param0 = new SqlParameter( "@img_name", SqlDbType.VarChar,50 );
        //  param0.Value = imgName;
        //  command.Parameters.Add( param0 ); 
        //  SqlParameter param1 = new SqlParameter( "@img_data", SqlDbType.Image );   
        //  param1.Value = imgbin; 
        //  command.Parameters.Add( param1 ); 
        //  connection.Open(); 
        //  int numRowsAffected = command.ExecuteNonQuery(); 
        //  connection.Close(); 
        //  return numRowsAffected; 
        //  } 
        //  private byte[] RetrieveFromDB(string lastname) 
        //  { 
        //  SqlConnection connection = new   SqlConnection("Server=(local);DataBase=Northwind; User Id=sa;Password=;");   SqlCommand command = new SqlCommand("select top 1 Photo from Employees 
        //    where lastname ='"+lastname +"'", connection ); 
        //  connection.Open(); 
        //  SqlDataReader dr = command.ExecuteReader(); 
        //  dr.Read(); 
        //  byte[] imgData = (byte[])dr["Photo"]; 
        //  connection.Close(); 
        //  return imgData;
        // }
        //}// end class 
        // [Serializable] 
        //  public class ClassToSerialize { 
        //  public string name; 
        //  public float[] fltArray; 
        //  // constructor initializes name and creates the sample array of floats 
        //  public ClassToSerialize(string theName) { 
        //  this.name=theName; 
        //  float[] theArray= new float[1000]; 
        //  Random rnd = new System.Random(); 
        //  for(int i =0;i<1000;i++) 
        //  theArray[i]=(float)rnd.NextDouble() *1000; 
        //  fltArray=theArray; 
        //  } 
        // } 
        //} 

        //}
    }
}