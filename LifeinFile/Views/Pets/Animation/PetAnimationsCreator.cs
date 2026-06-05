using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;

namespace LifeinFile.Views.Pets
{
    public class PetAnimationsCreator
    {
        PetModel _model;
        PetWindow _window;
        public PetAnimationsCreator(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
            
            Create();
        }

        MoveAnimation _move;
        BreathAnimation _breath;
        TurnAnimation _leftTurn;
        TurnAnimation _rightTurn;
       
        
        public MoveAnimation Move => _move;
        public BreathAnimation Breath => _breath;
        public TurnAnimation LeftTurn => _leftTurn;
        public TurnAnimation RightTurn => _rightTurn;
        void Create()
        {
            _move = new MoveAnimation(_model, _window);
            _breath = new BreathAnimation(_model, _window, _window);
            _leftTurn = new TurnAnimation(_window, Direction.Left);
            _rightTurn = new TurnAnimation(_window, Direction.Right);
        }
    }
}