using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net;

public class PacketIdentifier
{
    public static PacketIdentifier Instance { get; } = new();

    private Dictionary<(MinecraftVersion, PacketDirection, ConnectionState, int), MinecraftPacketType> _map = new();
    
    static PacketIdentifier()
    {
        
    }

    private PacketIdentifier()
    {
        Map1164Clientbound();
        Map1164Serverbound();
    }

    public void Map(MinecraftVersion version, PacketDirection direction, ConnectionState connectionState, int packetId, MinecraftPacketType type)
    {
        _map[(version, direction, connectionState, packetId)] = type;
    }

    public MinecraftPacketType Identify(MinecraftVersion version, PacketDirection direction, ConnectionState connectionState, int packetId)
    {
        if (_map.TryGetValue((version, direction, connectionState, packetId), out var packetType))
        {
            return packetType;
        }
        return MinecraftPacketType.NotImplemented;
        throw new Exception("packet with this id does not exist for this minecraft version");
    }
    
    private void Map1164Clientbound()
    {
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Login, 0x00, MinecraftPacketType.CB_Login_Disconnect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Login, 0x01, MinecraftPacketType.CB_Login_EncryptionRequest);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Login, 0x02, MinecraftPacketType.CB_Login_LoginSuccess);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Login, 0x03, MinecraftPacketType.CB_Login_SetCompression);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Login, 0x04, MinecraftPacketType.CB_Login_LoginPluginRequest);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x00, MinecraftPacketType.CB_Play_SpawnEntity);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x01, MinecraftPacketType.CB_Play_SpawnExperienceOrb);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x02, MinecraftPacketType.CB_Play_SpawnLivingEntity);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x03, MinecraftPacketType.CB_Play_SpawnPainting);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x04, MinecraftPacketType.CB_Play_SpawnPlayer);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x05, MinecraftPacketType.CB_Play_EntityAnimation);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x06, MinecraftPacketType.CB_Play_Statistics);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x07, MinecraftPacketType.CB_Play_AcknowledgePlayerDigging);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x08, MinecraftPacketType.CB_Play_BlockBreakAnimation);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x09, MinecraftPacketType.CB_Play_BlockEntityData);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0A, MinecraftPacketType.CB_Play_BlockAction);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0B, MinecraftPacketType.CB_Play_BlockChange);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0C, MinecraftPacketType.CB_Play_BossBar);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0D, MinecraftPacketType.CB_Play_ServerDifficulty);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0E, MinecraftPacketType.CB_Play_ChatMessage);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x0F, MinecraftPacketType.CB_Play_TabComplete);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x10, MinecraftPacketType.CB_Play_DeclareCommands);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x11, MinecraftPacketType.CB_Play_WindowConfirmation);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x12, MinecraftPacketType.CB_Play_CloseWindow);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x13, MinecraftPacketType.CB_Play_WindowItems);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x14, MinecraftPacketType.CB_Play_WindowProperty);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x15, MinecraftPacketType.CB_Play_SetSlot);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x16, MinecraftPacketType.CB_Play_SetCooldown);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x17, MinecraftPacketType.CB_Play_PluginMessage);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x18, MinecraftPacketType.CB_Play_NamedSoundEffect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x19, MinecraftPacketType.CB_Play_Disconnect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1A, MinecraftPacketType.CB_Play_EntityStatus);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1B, MinecraftPacketType.CB_Play_Explosion);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1C, MinecraftPacketType.CB_Play_UnloadChunk);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1D, MinecraftPacketType.CB_Play_ChangeGameState);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1E, MinecraftPacketType.CB_Play_OpenHorseWindow);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x1F, MinecraftPacketType.CB_Play_KeepAlive);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x20, MinecraftPacketType.CB_Play_ChunkData);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x21, MinecraftPacketType.CB_Play_Effect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x22, MinecraftPacketType.CB_Play_Particle);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x23, MinecraftPacketType.CB_Play_UpdateLight);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x24, MinecraftPacketType.CB_Play_JoinGame);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x25, MinecraftPacketType.CB_Play_MapData);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x26, MinecraftPacketType.CB_Play_TradeList);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x27, MinecraftPacketType.CB_Play_EntityPosition);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x28, MinecraftPacketType.CB_Play_EntityPositionRotation);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x29, MinecraftPacketType.CB_Play_EntityRotation);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2A, MinecraftPacketType.CB_Play_EntityMovement);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2B, MinecraftPacketType.CB_Play_VehicleMove);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2C, MinecraftPacketType.CB_Play_OpenBook);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2D, MinecraftPacketType.CB_Play_OpenWindow);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2E, MinecraftPacketType.CB_Play_OpenSignEditor);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x2F, MinecraftPacketType.CB_Play_CraftRecipeRepsonse);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x30, MinecraftPacketType.CB_Play_PlayerAbilities);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x31, MinecraftPacketType.CB_Play_CombatEvent);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x32, MinecraftPacketType.CB_Play_PlayerInfo);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x33, MinecraftPacketType.CB_Play_FacePlayer);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x34, MinecraftPacketType.CB_Play_PlayerPositionLook);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x35, MinecraftPacketType.CB_Play_UnlockRecipes);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x36, MinecraftPacketType.CB_Play_DestroyEntities);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x37, MinecraftPacketType.CB_Play_RemoveEntityEffect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x38, MinecraftPacketType.CB_Play_ResourcePackSend);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x39, MinecraftPacketType.CB_Play_Respawn);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3A, MinecraftPacketType.CB_Play_EntityHeadLook);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3B, MinecraftPacketType.CB_Play_MultiBlockChange);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3C, MinecraftPacketType.CB_Play_SelectAdvancementTab);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3D, MinecraftPacketType.CB_Play_WorldBorder);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3E, MinecraftPacketType.CB_Play_Camera);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x3F, MinecraftPacketType.CB_Play_HeldItemChange);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x40, MinecraftPacketType.CB_Play_UpdateViewPosition);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x41, MinecraftPacketType.CB_Play_UpdateViewDistance);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x42, MinecraftPacketType.CB_Play_SpawnPosition);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x43, MinecraftPacketType.CB_Play_DisplayScoreboard);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x44, MinecraftPacketType.CB_Play_EntityMetadata);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x45, MinecraftPacketType.CB_Play_AttachEntity);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x46, MinecraftPacketType.CB_Play_EntityVelocity);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x47, MinecraftPacketType.CB_Play_EntityEquipment);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x48, MinecraftPacketType.CB_Play_SetExperience);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x49, MinecraftPacketType.CB_Play_UpdateHealth);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4A, MinecraftPacketType.CB_Play_ScoreboardObjective);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4B, MinecraftPacketType.CB_Play_SetPassengers);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4C, MinecraftPacketType.CB_Play_Teams);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4D, MinecraftPacketType.CB_Play_UpdateScore);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4E, MinecraftPacketType.CB_Play_TimeUpdate);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x4F, MinecraftPacketType.CB_Play_Title);

        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x50, MinecraftPacketType.CB_Play_EntitySoundEffect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x51, MinecraftPacketType.CB_Play_SoundEffect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x52, MinecraftPacketType.CB_Play_StopSound);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x53, MinecraftPacketType.CB_Play_PlayerListHeaderFooter);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x54, MinecraftPacketType.CB_Play_NbtQueryReponse);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x55, MinecraftPacketType.CB_Play_CollectItem);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x56, MinecraftPacketType.CB_Play_EntityTeleport);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x57, MinecraftPacketType.CB_Play_Advancements);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x58, MinecraftPacketType.CB_Play_EntityProperties);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x59, MinecraftPacketType.CB_Play_EntityEffect);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x5A, MinecraftPacketType.CB_Play_DeclareRecipes);
        Map(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState.Play, 0x5B, MinecraftPacketType.CB_Play_Tags);
    }

    private void Map1164Serverbound()
    {

    }
}