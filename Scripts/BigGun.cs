using Godot;
using System;

public class BigGun : TextureRect
{
    int[] speedCost = {20,40,60,80,100};
    int[] piercingCost = {20,40,60,80,100};
    int[] heatingCost = {20,40,60,80,100};
    int[] damageCost = {20,40,60,80,100};
    
    int speedLevel,piercingLevel,heatingLevel,damageLevel;

    Label speedLabel, heatingLabel, piercingLabel, damageLabel;

    RobotoWeapon robotoWeapon;
    Roboto roboto;
    public override void _Ready()
    {
        speedLabel = GetNode("Speed/Label") as Label;
        heatingLabel = GetNode("Heating/Label") as Label;
        piercingLabel = GetNode("Piercing/Label") as Label;
        damageLabel = GetNode("Damage/Label") as Label;
        robotoWeapon = GetNode("../Roboto/RobotoWeapon") as RobotoWeapon;
        roboto = GetNode("../Roboto") as Roboto;
    }

    public override void _Process(float delta)
    {
        if(GetNode<Main>("..").waveDuration.TimeLeft == 0 &&
           GetNode<KinematicBody2D>("../Roboto").Position.DistanceTo(new Vector2(0,0)) < 64&&
           GetNode<Main>("..").stoped)
        {
            Visible = true;
        }
        else
        {
            Visible = false;
        }
    }

    public void _on_Done_button_up()
    {
        if(GetNode<Main>("..").waveDuration.TimeLeft == 0 &&
           GetNode<KinematicBody2D>("../Roboto").Position.DistanceTo(new Vector2(0,0)) < 64)
        {
            GetNode<Main>("..").waveDuration.Start();
            GetNode<Main>("..").stoped = false;
        }
    }
    public void _on_Speed_button_up()
    {
        if(robotoWeapon != null && roboto != null)
        {
            if(roboto.hp >= speedCost[speedLevel] && speedLevel < 5)
            {
                roboto.hp -= speedCost[speedLevel];
                speedLevel++;
                robotoWeapon.timer.WaitTime = 0.45f/(speedLevel+1);
            }   
            GD.Print("Speed");
            if(speedLevel < 5)
                speedLabel.Text = ((speedLevel+1)*100+100).ToString()+"% Speed\n-"+speedCost[speedLevel].ToString()+"HP";
            else
                speedLabel.Text = " ";
        }
        else GD.Print("RobotoWeapon or roboto is null");
        
    }

    public void _on_Heating_button_up()
    {
        if(robotoWeapon != null && roboto != null)
        {
            if(roboto.hp >= heatingCost[heatingLevel] && heatingLevel < 5)
            {
                roboto.hp -= heatingCost[heatingLevel];
                heatingLevel++;
                robotoWeapon.overload -= 0.2f;
            }
            if(heatingLevel < 5)
                heatingLabel.Text = (100 - (heatingLevel+1)*0.2f*100).ToString()+"% Heating\n-"+heatingCost[heatingLevel].ToString()+"HP";
            else
                heatingLabel.Text = " ";
            GD.Print("Heating");
        }
        else GD.Print("RobotoWeapon or roboto is null");  
    }
    public void _on_Piercing_button_up()
    {
        if(robotoWeapon != null && roboto != null)
        {if(roboto.hp >= piercingCost[piercingLevel] && piercingLevel < 5)
            {
                roboto.hp -= piercingCost[piercingLevel];
                piercingLevel++;
                robotoWeapon.piercing = piercingLevel+1;
            }   
            if(piercingLevel < 5)
                piercingLabel.Text = "+1 Piercing\n-"+piercingCost[piercingLevel].ToString()+"HP";
            else
                piercingLabel.Text = " ";
            GD.Print("Piercing");
        }
        else GD.Print("RobotoWeapon or roboto is null");
        
    }

    public void _on_Damage_button_up()
    {
        if(robotoWeapon != null && roboto != null)
        {if(roboto.hp >= damageCost[damageLevel] && damageLevel < 5)
            {
                roboto.hp -= damageCost[damageLevel];
                damageLevel++;
                robotoWeapon.damage = 3*(damageLevel+1);
            }   
            if(damageLevel < 5)
                damageLabel.Text = "+3 Damage \n-"+damageCost[damageLevel].ToString()+"HP";
            else
                damageLabel.Text = " ";
            GD.Print("Damage");
        }
        else GD.Print("RobotoWeapon or roboto is null");
        
    }


}
