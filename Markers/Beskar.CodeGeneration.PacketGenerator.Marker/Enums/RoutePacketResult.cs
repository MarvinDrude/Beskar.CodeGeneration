namespace Beskar.CodeGeneration.PacketGenerator.Marker.Enums;

public enum RoutePacketResult : byte
{
   Success = 0,
   SuccessNoHandlers = 1,
   NotEnoughData = 2,
   UnknownPacket = 3,
   InvalidPacket = 4
}