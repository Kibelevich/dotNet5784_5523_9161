namespace BITest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        static void Main(string[] args)
        {
            BO.Engineer a = new BO.Engineer { email="hjk",ID= 123,name="sfgh",cost=123,level=(BO.EngineerExperiece)2, currentTask=null}; 
            s_bl.Engineer.Create(a);
                }
    }
}