/*
Script by sven from ambiera forum.

For more stuff visit my itch page:
https://5v3n.itch.io/

I provide this script for free but any donation will be
appreciated and will help me to keep makeing more cool stuff.

You can support my work by using PayPal.
PayPal: PayPal.Me/idid4516

*/

/*
<behavior jsname="behavior_svcHealthBar" description="svcHealthBar">
<property name="Actor" type="scenenode" />
<property name="HealthBar" type="scenenode" />
<property name="HideDistance" type="float" default="200.0"/>
</behavior>
*/

acID=0; //rename 

behavior_svcHealthBar = function()
{
this.W=null;
this.HPW=null;
}

behavior_svcHealthBar.prototype.onAnimate = function(currentNode)
{
var cp=ccbGetSceneNodeProperty(ccbGetActiveCamera(), "PositionAbs");
var np=ccbGetSceneNodeProperty(this.Actor, "PositionAbs");
var d=cp.substract(np).getLength();

if(d>this.HideDistance)
{
ccbSetSceneNodeProperty(this.HealthBar, "Visible",false);
return 1;
}else{
ccbSetSceneNodeProperty(this.HealthBar, "Visible",true);
}

if(this.W==null)
{

var tn=ccbGetSceneNodeProperty(this.Actor, "Name") //rename

acID+=1;//rename
ccbSetSceneNodeProperty(this.Actor, "Name" ,tn+ "ff" +acID);//rename -fix issue with clones.. adds some more issues in some cases.

this.W=ccbGetSceneNodeProperty(this.HealthBar, "Width");
}

var my_health_name = "#" + ccbGetSceneNodeProperty(this.Actor, "Name") + ".health";
var my_health=ccbGetCopperCubeVariable(my_health_name);

if(this.HPW==null)
{
this.HPW=my_health;
}

var HP = (my_health / this.HPW) * this.W;

if(HP<=0)
{
HP=0.001;
}

ccbSetSceneNodeProperty(this.HealthBar, "Width",HP);

return true;
}
