using Machine.Specifications;
using PassSharp;
using PassSharp.Fields;

#pragma warning disable 414

namespace Test
{

  [Subject ("Pass Generation")]
  public class when_generating_a_pass
  {
    static Pass pass;

    Establish context = () => {
      pass = new Pass ();
    };

    public class when_adding_a_signature
    {

    }

  }

}
