using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Enums
{
    public enum ClientboundPlayPacketType
    {
        SpawnEntityPacket = 0x00,
        SpawnExperienceOrbPacket = 0x01,
        SpawnLivingEntityPacket = 0x02,
        SpawnPaintingPacket = 0x03,
        SpawnPlayerPacket = 0x04,
        EntityAnimationPacket = 0x05,
        StatisticsPacket = 0x06,
        AcknowledgePlayerDiggingPacket = 0x07,
        BlockBreakAnimationPacket = 0x08,
        BlockEntityDataPacket = 0x09,
        BlockActionPacket = 0x0A,
        BlockChangePacket = 0x0B,
        BossBarPacket = 0x0C,
        ServerDifficultyPacket = 0x0D,
        ChatMessagePacket = 0x0E,
        TabCompletePacket = 0x0F,

        DeclareCommandsPacket = 0x10,
        WindowConfirmationPacket = 0x11,
        CloseWindowPacket = 0x12,
        WindowItemsPacket = 0x13,
        WindowPropertyPacket = 0x14,
        SetSlotPacket = 0x15,
        SetCooldownPacket = 0x16,
        PluginMessagePacket = 0x17,
        NamedSoundEffectPacket = 0x18,
        DisconnectPacket = 0x19,
        EntityStatusPacket = 0x1A,
        ExplosionPacket = 0x1B,
        UnloadChunkPacket = 0x1C,
        ChangeGameStatePacket = 0x1D,
        OpenHorseWindowPacket = 0x1E,
        KeeplAlivePacket = 0x1F,

        ChunkDataPacket = 0x20,
        EffectPacket = 0x21,
        ParticlePacket = 0x22,
        UpdateLightPacket = 0x23,
        JoinGamePacket = 0x24,
        MapDataPacket = 0x25,
        TradeListPackets = 0x26,
        EntityPositionPacket = 0x27,
        EntityPositionRotationPacket = 0x28,
        EntityRotationPacket = 0x29,
        EntityMovementPacket = 0x2A,
        VehicleMovePacket = 0x2B,
        OpenBookPacket = 0x2C,
        OpenWindowPacket = 0x2D,
        OpenSignEditorPacket = 0x2E,
        CraftRecipeResponsePacket = 0x2F,

        PlayerAbilitiesPacket = 0x30,
        CombatEventPacket = 0x31,
        PlayerInfoPacket = 0x32,
        FacePlayerPacket = 0x33,
        PlayerPositionLookPacket = 0x34,
        UnlockRecipesPacket = 0x35,
        DestroyEntitiesPacket = 0x36,
        RemoveEntityEffectPacket = 0x37,
        ResourcePackSendPacket = 0x38,
        RespawnPacket = 0x39,
        EntityHeadLookPacket = 0x3A,
        MultiBlockChangePacket = 0x3B,
        SelectAdvancementTabPacket = 0x3C,
        WorldBorderPacket = 0x3D,
        CameraPacket = 0x3E,
        HeldItemChangePacket = 0x3F
    }
}
