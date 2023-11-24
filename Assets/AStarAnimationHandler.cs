using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class AStarAnimationHandler : MonoBehaviour
{

    [SerializeField] private AStarPathfinder pathfinder;
    [SerializeField] private SpriteLibraryAsset upSpriteLibraryAsset;
    [SerializeField] private SpriteLibraryAsset sideSpriteLibraryAsset;
    [SerializeField] private SpriteLibraryAsset downSpriteLibraryAsset;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SpriteResolver spriteResolver;
    
    private static readonly int SpeedMultiplierAnimHash = Animator.StringToHash("SpeedMultiplier");
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = pathfinder.currentVelocity;
        animator.SetFloat(SpeedMultiplierAnimHash, velocity.magnitude);

        if (upSpriteLibraryAsset && sideSpriteLibraryAsset && downSpriteLibraryAsset)
        {
            if (Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x))
            {
                if (velocity.y > 0) spriteResolver.spriteLibrary.spriteLibraryAsset = upSpriteLibraryAsset;
                else spriteResolver.spriteLibrary.spriteLibraryAsset = downSpriteLibraryAsset;
            } else spriteResolver.spriteLibrary.spriteLibraryAsset = sideSpriteLibraryAsset;
        }

        if (velocity.x is < 0.1f and > -0.1f) return;
        spriteRenderer.flipX = velocity.x < 0;
    }
}
