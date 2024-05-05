using Content.Server.Lightning;
using Content.Server.Tesla.Components;
using Content.Server.Tesla.Components;
using Content.Server.Tesla.EntitySystems;
using Robust.Shared.Prototypes;
using Content.Shared.Chemistry.Reagent;

namespace Content.Server.Chemistry.ReactionEffects;


[DataDefinition]
public sealed partial class LightningReactionEffect : ReagentEffect
{
    /// <summary>
    ///     Impulse range per unit of reagent
    /// </summary>
    [DataField("maxLightningArc")]
    public int MaxLightningArc = 1;

    /// <summary>
    ///     Maximum impulse range
    /// </summary>
    [DataField("shootMinInterval")]
    public float ShootMinInterval = 0.5f;

    /// <summary>
    ///     How much energy will be drain from sources
    /// </summary>
    [DataField]
    public float ShootMaxInterval = 8.0f;

    /// <summary>
    ///     Amount of time entities will be disabled
    /// </summary>
    [DataField("shootRange")]
    public float ShootRange = 5f;

    [DataField("arcDepth")]
    public int ArcDepth = 1;

    [DataField]
    public TimeSpan NextShootTime;

    [DataField("lightningPrototype")]
    public EntProtoId LightningPrototype = "Lightning";

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
            => Loc.GetString("reagent-effect-guidebook-lightning-reaction-effect", ("chance", Probability));

    public override void Effect(ReagentEffectArgs args)
    {
        var transform = args.EntityManager.GetComponent<TransformComponent>(args.SolutionEntity);
        var range = MathF.Min((float) (args.Quantity*ArcDepth), MaxLightningArc);

        args.EntityManager.System<LightningSystem>().ShootRandomLightnings(
            args.SolutionEntity,
            range,
            ArcDepth);
    }
}
