using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using exelent;
using static exelent.Structs;

namespace exelent
{
    internal class GrenadeTrace
    {
        private static string[] nades = { "C_SmokeGrenade", "C_MolotovGrenade", "C_HEGrenade", "C_Flashbang", "C_DecoyGrenade", "C_IncendiaryGrenade" };

        internal static bool IsGrenade(string classname)
        {
            if (Array.Exists(nades, element => element == classname))
            {
                //Console.WriteLine("Aranan string dizide bulundu!");
                return true;
            }
            else
            {
                //Console.WriteLine("Aranan string dizide bulunamadı!");
                return true;
            }
        }

        internal static void trace(OverlayRenderer render, Size WindowSize, GlobalVarsBase globals, float[] viewmatrix, UInt64 localplayer)
        {
            Vector3 viewangles = memory.Read<Vector3>(memory.client + offsets.dwViewAngles);
            Vector3 startingPosition = memory.Read<Vector3>(localplayer + offsets.m_vOldOrigin);
            float speed = 10f; // Bomba hızı
            float gravity = 199.8f; // Yerçekimi

            double[] position = { startingPosition.X, startingPosition.Y, startingPosition.Z }; // Örnek bir pozisyon vektörü
            double[] viewAngles = { viewangles.X, viewangles.Y, viewangles.Z }; // Örnek bir görüş açısı vektörü (örneğin: yaw, pitch, roll)

            // Görüş açılarını dereceden radyana çevirme
            double yaw = DegreeToRadian(viewAngles[0]);
            double pitch = DegreeToRadian(viewAngles[1]);

            // Yön vektörünü hesaplama
            Vector3 direction = new Vector3((float)(Math.Cos(pitch) * Math.Cos(yaw)), (float)(Math.Cos(pitch) * Math.Sin(yaw)), (float)-Math.Sin(pitch));

            startingPosition += Simulate(1, direction, speed, gravity);

            
            if (Form1.DXWorldToScreen(startingPosition, out Vector2 pos, viewmatrix, WindowSize.Width, WindowSize.Height))
            {
                float BoxHeight = pos.Y - pos.Y + 5;
                float BoxWidth = BoxHeight / 2.4f;
                float x1 = pos.X - (BoxWidth / 2f);
                float y1 = pos.Y;

                render.Rectangle((int)x1, (int)y1, (int)BoxWidth, (int)BoxHeight, Color.DarkBlue, false, true);
            }
            
        }

        static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        static Vector3 Simulate(float deltaTime, Vector3 direction, float speed, float gravity)
        {
            // Fizik simülasyonu - konumu güncelle
            Vector3 displacement = direction * speed * deltaTime; // Hareket miktarı
            float gravityDisplacement = 0.5f * gravity * deltaTime * deltaTime; // Yerçekimi etkisi

            // Hareketi güncelle
            displacement.Y -= gravityDisplacement; // Yerçekimi etkisi ekle

            return displacement;
        }

        static bool physics_simulate(GlobalVarsBase globals)
        {




            return true;
        }


    }

            /*
        var sv_gravity = 800/*csgo.m_engine_cvars()->FindVar("sv_gravity");
    Vector3 m_velocity = new Vector3();
    var new_velocity_z = m_velocity.Z - (sv_gravity * 0.4f) * globals.interval_per_tick;

    var move = new Vector3(m_velocity.X * globals.interval_per_tick, m_velocity.Y * globals.interval_per_tick, (m_velocity.Z + new_velocity_z) / 2f * globals.interval_per_tick);


    Vector3 origin = memory.Read<Vector3>(localplayer + offsets.m_vOldOrigin);

    origin += move;
            //Console.WriteLine(move);
            */




    /*
    
    #pragma once
 
#include "../../core/source.hpp"
#include "../../sdk/sdk.hpp"
#include "../../sdk/props/player.hpp"
#include "../../sdk/props/weapon.hpp"
#include "../../core/hooks/hooked.hpp"
#include <misc/movement.hpp>
#include <deque>
template<typename T>
class Singleton
{
protected:
	Singleton() {}
	~Singleton() {}
 
	Singleton(const Singleton&) = delete;
	Singleton& operator=(const Singleton&) = delete;
 
	Singleton(Singleton&&) = delete;
	Singleton& operator=(Singleton&&) = delete;
 
public:
	static T& Get()
	{
		static T inst{};
		return inst;
	}
};
 
class c_grenade_prediction : public Singleton< c_grenade_prediction > {
public:
	struct data_t {
		__forceinline data_t() = default;
 
		__forceinline data_t(C_BasePlayer* owner, int index, const Vector& origin, const Vector& velocity, float throw_time, int offset) : data_t() {
			m_owner = owner;
			m_index = index;
 
			predict(origin, velocity, throw_time, offset);
		}
 
		__forceinline bool physics_simulate() {
			if (m_detonated)
				return true;
 
			static const auto sv_gravity = csgo.m_engine_cvars()->FindVar("sv_gravity");
 
			const auto new_velocity_z = m_velocity.z - (sv_gravity->GetFloat() * 0.4f) * csgo.m_globals()->interval_per_tick;
 
			const auto move = Vector(
				m_velocity.x * csgo.m_globals()->interval_per_tick,
				m_velocity.y * csgo.m_globals()->interval_per_tick,
				(m_velocity.z + new_velocity_z) / 2.f * csgo.m_globals()->interval_per_tick
			);
 
			m_velocity.z = new_velocity_z;
 
			auto trace = trace_t();
 
			physics_push_entity(move, trace);
 
			if (m_detonated)
				return true;
 
			if (trace.fraction != 1.f) {
				update_path< true >();
 
				perform_fly_collision_resolution(trace);
			}
 
			return false;
		}
 
		__forceinline void physics_trace_entity(const Vector& src, const Vector& dst, std::uint32_t mask, trace_t& trace) {
			csgo.m_engine_trace()->TraceHull(
				src, dst, { -2.f, -2.f, -2.f }, { 2.f, 2.f, 2.f },
				mask, m_owner, m_collision_group, &trace
			);
 
			if (trace.startsolid
				&& (trace.contents & CONTENTS_CURRENT_90)) {
				trace.clear();
 
				csgo.m_engine_trace()->TraceHull(
					src, dst, { -2.f, -2.f, -2.f }, { 2.f, 2.f, 2.f },
					mask & ~CONTENTS_CURRENT_90, m_owner, m_collision_group, &trace
				);
			}
 
			if (!trace.DidHit()
				|| !trace.m_pEnt
				|| !reinterpret_cast<C_BaseEntity*>(trace.m_pEnt)->IsPlayer())
				return;
 
			trace.clear();
 
			csgo.m_engine_trace()->TraceLine(src, dst, mask, m_owner, m_collision_group, &trace);
		}
 
		__forceinline void physics_push_entity(const Vector& push, trace_t& trace) {
			physics_trace_entity(m_origin, m_origin + push,
				m_collision_group == COLLISION_GROUP_DEBRIS
				? (MASK_SOLID | CONTENTS_CURRENT_90) & ~CONTENTS_MONSTER
				: MASK_SOLID | CONTENTS_OPAQUE | CONTENTS_IGNORE_NODRAW_OPAQUE | CONTENTS_CURRENT_90 | CONTENTS_HITBOX,
				trace
			);
 
			if (trace.startsolid) {
				m_collision_group = COLLISION_GROUP_INTERACTIVE_DEBRIS;
 
				csgo.m_engine_trace()->TraceLine(
					m_origin - push, m_origin + push,
					(MASK_SOLID | CONTENTS_CURRENT_90) & ~CONTENTS_MONSTER,
					m_owner, m_collision_group, &trace
				);
			}
 
			if (trace.fraction) {
				m_origin = trace.endpos;
			}
 
			if (!trace.m_pEnt)
				return;
 
			if (reinterpret_cast<C_BaseEntity*>(trace.m_pEnt)->IsPlayer()
				|| m_index != WEAPON_TAGRENADE && m_index != WEAPON_MOLOTOV && m_index != WEAPON_INCGRENADE)
				return;
 
			static const auto weapon_molotov_maxdetonateslope = csgo.m_engine_cvars()->FindVar("weapon_molotov_maxdetonateslope");
 
			if (m_index != WEAPON_TAGRENADE
				&& trace.plane.normal.z < std::cos(DEG2RAD(weapon_molotov_maxdetonateslope->GetFloat())))
				return;
 
			detonate< true >();
		}
 
		__forceinline void perform_fly_collision_resolution(trace_t& trace) {
			auto surface_elasticity = 1.f;
 
			if (trace.m_pEnt) {
#if 0 /* ayo reis wtf 
				if (const auto v8 = trace.m_surface.m_name) {
					if (*(DWORD*) v8 != 'spam' || * ((DWORD*) v8 + 1) != '_sc/') {
						if (*((DWORD*) v8 + 1) == '_ed/'
							&& * ((DWORD*) v8 + 2) == 'ekal'
							&& * ((DWORD*) v8 + 3) == 'alg/'
							&& * ((DWORD*) v8 + 4) == 'g/ss'
							&& * ((DWORD*) v8 + 5) == 'ssal') {
							goto BREAKTROUGH;
						}
					}

                    else if (*((DWORD*)v8 + 2) == 'iffo'
                        && *((DWORD*)v8 + 3) == 'g/ec'
                        && *((DWORD*)v8 + 4) == 'ssal'
                        && *((DWORD*)v8 + 5) == 'bru/'
                        && *((DWORD*)v8 + 6) == 'g_na'
                        && *((DWORD*)v8 + 7) == 'ssal')
{
    goto BREAKTROUGH;
}
				}
#endif
				if (reinterpret_cast<C_BaseEntity*>(trace.m_pEnt)->is_breakable())
{
BREAKTROUGH:
    m_velocity *= 0.4f;

    return;
}

const auto is_player = reinterpret_cast<C_BaseEntity*>(trace.m_pEnt)->IsPlayer();
if (is_player)
{
    surface_elasticity = 0.3f;
}

if (trace.m_pEnt->entindex())
{
    if (is_player
        && m_last_hit_entity == trace.m_pEnt)
    {
        m_collision_group = COLLISION_GROUP_DEBRIS;
        return;
    }

    m_last_hit_entity = trace.m_pEnt;
}
			}
 
			auto velocity = Vector();

const auto back_off = m_velocity.Dot(trace.plane.normal) * 2.f;

for (auto i = 0u; i < 3u; i++)
{
    const auto change = trace.plane.normal[i] * back_off;

    velocity[i] = m_velocity[i] - change;

    if (std::fabs(velocity[i]) >= 1.f)
        continue;

    velocity[i] = 0.f;
}

velocity *= std::clamp<float>(surface_elasticity * 0.45f, 0.f, 0.9f);

if (trace.plane.normal.z > 0.7f)
{
    const auto speed_sqr = velocity.LengthSquared();
    if (speed_sqr > 96000.f)
    {
        const auto l = velocity.Normalized().Dot(trace.plane.normal);
        if (l > 0.5f)
        {
            velocity *= 1.f - l + 0.5f;
        }
    }

    if (speed_sqr < 400.f)
    {
        m_velocity = Vector(0, 0, 0);
    }
    else
    {
        m_velocity = velocity;

        physics_push_entity(velocity * ((1.f - trace.fraction) * csgo.m_globals()->interval_per_tick), trace);
    }
}
else
{
    m_velocity = velocity;

    physics_push_entity(velocity * ((1.f - trace.fraction) * csgo.m_globals()->interval_per_tick), trace);
}

if (m_bounces_count > 20)
    return detonate < false > ();

++m_bounces_count;
		}
 
		__forceinline void think() {
    switch (m_index)
    {
        case WEAPON_SMOKEGRENADE:
            if (m_velocity.LengthSquared() <= 0.01f)
            {
                detonate < false > ();
            }

            break;
        case WEAPON_DECOY:
            if (m_velocity.LengthSquared() <= 0.04f)
            {
                detonate < false > ();
            }

            break;
        case WEAPON_FLASHBANG:
        case WEAPON_HEGRENADE:
        case WEAPON_MOLOTOV:
        case WEAPON_INCGRENADE:
            if (TICKS_TO_TIME(m_tick) > m_detonate_time)
            {
                detonate < false > ();
            }

            break;
    }

    m_next_think_tick = m_tick + TIME_TO_TICKS(0.2f);
}

template < bool _bounced >
__forceinline void detonate()
{
    m_detonated = true;

    update_path<_bounced>();
}

template < bool _bounced >
__forceinline void update_path()
{
    m_last_update_tick = m_tick;

    m_path.emplace_back(m_origin, _bounced);
}

__forceinline void predict(const Vector& origin, const Vector& velocity, float throw_time, int offset) {
    m_origin = origin;
    m_velocity = velocity;
    m_collision_group = COLLISION_GROUP_PROJECTILE;

    const auto tick = TIME_TO_TICKS(1.f / 30.f);

    m_last_update_tick = -tick;

    switch (m_index)
    {
        case WEAPON_SMOKEGRENADE: m_next_think_tick = TIME_TO_TICKS(1.5f); break;
        case WEAPON_DECOY: m_next_think_tick = TIME_TO_TICKS(2.f); break;
        case WEAPON_FLASHBANG:
        case WEAPON_HEGRENADE:
            m_detonate_time = 1.5f;
            m_next_think_tick = TIME_TO_TICKS(0.02f);

            break;
        case WEAPON_MOLOTOV:
        case WEAPON_INCGRENADE:
            static const auto molotov_throw_detonate_time = csgo.m_engine_cvars()->FindVar("molotov_throw_detonate_time");

            m_detonate_time = molotov_throw_detonate_time->GetFloat();
            m_next_think_tick = TIME_TO_TICKS(0.02f);

            break;
    }

    for (; m_tick < TIME_TO_TICKS(60.f); ++m_tick)
    {
        if (m_next_think_tick <= m_tick)
        {
            think();
        }

        if (m_tick < offset)
            continue;

        if (physics_simulate())
            break;

        if (m_last_update_tick + tick > m_tick)
            continue;

        update_path < false > ();
    }

    if (m_last_update_tick + tick <= m_tick)
    {
        update_path < false > ();
    }

    m_expire_time = throw_time + TICKS_TO_TIME(m_tick);
}

bool draw() const;

bool m_detonated { };
C_BasePlayer* m_owner { };
Vector m_origin { }, m_velocity{};
IClientEntity* m_last_hit_entity { };
Collision_Group_t m_collision_group { };
float m_detonate_time { }, m_expire_time{};
int m_index { }, m_tick{}, m_next_think_tick{},
			m_last_update_tick{}, m_bounces_count{};
std::vector<std::pair<Vector, bool>> m_path { };
	} m_data{};

std::unordered_map < unsigned long, data_t > m_list{};
public:
	__forceinline c_grenade_prediction() = default;

void on_create_move(CUserCmd* cmd);

__forceinline const data_t& get_local_data() const { return m_data; }
 
	__forceinline std::unordered_map < unsigned long, data_t >& get_list() { return m_list; }

virtual void grenade_warning(C_BasePlayer* entity);
};

*/
}
