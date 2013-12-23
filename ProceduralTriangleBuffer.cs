using System;
using System.Collections.Generic;
using System.Text;

using Mogre;
using Math=Mogre.Math;

namespace Mogre_Procedural
{
//* This is ogre-procedural's temporary mesh buffer.
// * It stores all the info needed to build an Ogre Mesh, yet is intented to be more flexible, since
// * there is no link towards hardware.
// 
//C++ TO C# CONVERTER WARNING: The original type declaration contained unconverted modifiers:
//ORIGINAL LINE: class _ProceduralExport TriangleBuffer
public class TriangleBuffer
{
	public class Vertex
	{
		public Vector3 mPosition = new Vector3();
		public Vector3 mNormal = new Vector3();
		public Vector2 mUV = new Vector2();
	}

	protected List<int> mIndices = new List<int>();

	protected List<Vertex> mVertices = new List<Vertex>();
	//std::vector<Vertex>::iterator mCurrentVertex;
	protected int globalOffset;
	protected int mEstimatedVertexCount;
	protected int mEstimatedIndexCount;
	protected Vertex mCurrentVertex;


	public TriangleBuffer()
	{
		globalOffset = 0;
		mEstimatedVertexCount = 0;
		mEstimatedIndexCount = 0;
		mCurrentVertex = null;
	}

	public void append(TriangleBuffer STLAllocator<U, AllocPolicy>)
	{
		rebaseOffset();
		for (List<int>.Enumerator it = STLAllocator<U, AllocPolicy>.mIndices.GetEnumerator(); it != STLAllocator<U, AllocPolicy>.mIndices.end(); ++it)
		{
			mIndices.Add(globalOffset+ (it.Current));
		}
		for (List<Vertex>.Enumerator it = STLAllocator<U, AllocPolicy>.mVertices.GetEnumerator(); it != STLAllocator<U, AllocPolicy>.mVertices.end(); ++it)
		{
			mVertices.Add(it.Current);
		}
	}

	/// Gets a modifiable reference to vertices
	public List<Vertex> getVertices()
	{
		return mVertices;
	}

	/// Gets a non-modifiable reference to vertices
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const List<Vertex>& getVertices() const
	public List<Vertex> getVertices()
	{
		return mVertices;
	}

	/// Gets a modifiable reference to vertices
	public List<int> getIndices()
	{
		return mIndices;
	}

	/// Gets a non-modifiable reference to indices
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const List<int>& getIndices() const
	public List<int> getIndices()
	{
		return mIndices;
	}

//    *
//	 * Rebase index offset : call that function before you add a new mesh to the triangle buffer
//	 
	public void rebaseOffset()
	{
		globalOffset = mVertices.Count;
	}

//    *
//	 * Builds an Ogre Mesh from this buffer.
//	 
	public MeshPtr transformToMesh(string name)
	{
		return transformToMesh(name, "General");
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Ogre::MeshPtr transformToMesh(const string& name, const Ogre::String& group = "General") const
//C++ TO C# CONVERTER NOTE: Overloaded method(s) are created above to convert the following method having default parameters:
	public MeshPtr transformToMesh(string name, string group)
	{
		Mogre.SceneManager sceneMgr = Root.getSingleton().getSceneManagerIterator().begin().second;
		Mogre.ManualObject manual = sceneMgr.createManualObject();
		manual.begin("BaseWhiteNoLighting", RenderOperation.OperationType.OT_TRIANGLE_LIST);
	
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			manual.position(it.mPosition);
			manual.textureCoord(it.mUV);
			manual.normal(it.mNormal);
		}
		for (List<int>.Enumerator it = mIndices.GetEnumerator(); it.MoveNext(); ++it)
		{
			manual.index(it.Current);
		}
		manual.end();
		Mogre.MeshPtr mesh = manual.convertToMesh(name, group);
	
		sceneMgr.destroyManualObject(manual);
	
		return mesh;
	}

	//* Adds a new vertex to the buffer 
	public TriangleBuffer vertex(Vertex v)
	{
		mVertices.Add(v);
		mCurrentVertex = mVertices[mVertices.Count - 1];
		return this;
	}

	//* Adds a new vertex to the buffer 
	public TriangleBuffer vertex(Vector3 position, Vector3 normal, Vector2 uv)
	{
		Vertex v = new Vertex();
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: v.mPosition = position;
		v.mPosition.CopyFrom(position);
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: v.mNormal = normal;
		v.mNormal.CopyFrom(normal);
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: v.mUV = uv;
		v.mUV.CopyFrom(uv);
		mVertices.Add(v);
		mCurrentVertex = mVertices[mVertices.Count - 1];
		return this;
	}

	//* Adds a new vertex to the buffer 
	public TriangleBuffer position(Vector3 pos)
	{
		Vertex v = new Vertex();
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: v.mPosition = pos;
		v.mPosition.CopyFrom(pos);
		mVertices.Add(v);
		mCurrentVertex = mVertices[mVertices.Count - 1];
		return this;
	}

	//* Adds a new vertex to the buffer 
	public TriangleBuffer position(float x, float y, float z)
	{
		Vertex v = new Vertex();
		v.mPosition = Vector3(x, y, z);
		mVertices.Add(v);
		mCurrentVertex = mVertices[mVertices.Count - 1];
		return this;
	}

	//* Sets the normal of the current vertex 
	public TriangleBuffer normal(Vector3 normal)
	{
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: mCurrentVertex->mNormal = normal;
		mCurrentVertex.mNormal.CopyFrom(normal);
		return this;
	}

	//* Sets the texture coordinates of the current vertex 
	public TriangleBuffer textureCoord(float u, float v)
	{
		mCurrentVertex.mUV = Vector2(u, v);
		return this;
	}

	//* Sets the texture coordinates of the current vertex 
	public TriangleBuffer textureCoord(Vector2 vec)
	{
//C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: mCurrentVertex->mUV = vec;
		mCurrentVertex.mUV.CopyFrom(vec);
		return this;
	}

//    *
//	 * Adds an index to the index buffer.
//	 * Index is relative to the latest rebaseOffset().
//	 
	public TriangleBuffer index(int i)
	{
		mIndices.Add(globalOffset+i);
		return this;
	}

//    *
//	 * Adds a triangle to the index buffer.
//	 * Index is relative to the latest rebaseOffset().
//	 
	public TriangleBuffer triangle(int i1, int i2, int i3)
	{
		mIndices.Add(globalOffset+i1);
		mIndices.Add(globalOffset+i2);
		mIndices.Add(globalOffset+i3);
		return this;
	}

	/// Applies a matrix to transform all vertices inside the triangle buffer
	public TriangleBuffer applyTransform(Mogre.Matrix4 matrix)
	{
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			it.mPosition = matrix * it.mPosition;
			it.mNormal = matrix * it.mNormal;
			it.mNormal.normalise();
		}
		return this;
	}

	/// Applies the translation immediately to all the points contained in that triangle buffer
	/// @param amount translation vector
	public TriangleBuffer translate(Vector3 amount)
	{
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			it.mPosition += amount;
		}
		return this;
	}

	/// Applies the translation immediately to all the points contained in that triangle buffer
	public TriangleBuffer translate(float x, float y, float z)
	{
		return translate(Vector3(x, y, z));
	}

	/// Applies the rotation immediately to all the points contained in that triangle buffer
	/// @param quat the rotation quaternion to apply
	public TriangleBuffer rotate(Mogre.Quaternion quat)
	{
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			it.mPosition = quat * it.mPosition;
			it.mNormal = quat * it.mNormal;
			it.mNormal.normalise();
		}
		return this;
	}

	/// Applies an immediate scale operation to that triangle buffer
	/// @param scale Scale vector
	public TriangleBuffer scale(Vector3 scale)
	{
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			it.mPosition = scale * it.mPosition;
		}
		return this;
	}

	/// Applies an immediate scale operation to that triangle buffer
	/// @param x X scale component
	/// @param y Y scale component
	/// @param z Z scale component
	public TriangleBuffer scale(float x, float y, float z)
	{
		return scale(Vector3(x, y, z));
	}

	/// Applies normal inversion on the triangle buffer
	public TriangleBuffer invertNormals()
	{
		for (List<Vertex>.Enumerator it = mVertices.GetEnumerator(); it.MoveNext(); ++it)
		{
			it.mNormal = -it.mNormal;
		}
		for (uint i =0; i < mIndices.Count; ++i)
		{
			if (i%3==1)
			{
				std.swap(mIndices[i], mIndices[i-1]);
			}
		}
		return this;
	}

//    *
//	 * Gives an estimation of the number of vertices need for this triangle buffer.
//	 * If this function is called several times, it means an extra vertices count, not an absolute measure.
//	 
	public void estimateVertexCount(uint vertexCount)
	{
		mEstimatedVertexCount += vertexCount;
		mVertices.Capacity = mEstimatedVertexCount;
	}

//    *
//	 * Gives an estimation of the number of indices needed for this triangle buffer.
//	 * If this function is called several times, it means an extra indices count, not an absolute measure.
//	 
	public void estimateIndexCount(uint indexCount)
	{
		mEstimatedIndexCount += indexCount;
		mIndices.Capacity = mEstimatedIndexCount;
	}
}


//void TriangleBuffer::importEntity(Entity* entity)
//	{
//		bool added_shared = false;
//	int current_offset = 0;
//	int shared_offset = 0;
//	int next_offset = 0;
//	int index_offset = 0;
//	int vertex_count = 0;
//	int index_count = 0;
//
//	Ogre::MeshPtr mesh = entity->getMesh();
//
//
//	bool useSoftwareBlendingVertices = entity->hasSkeleton();
//
//	if (useSoftwareBlendingVertices)
//	  entity->_updateAnimation();
//
//    // Calculate how many vertices and indices we're going to need
//	for (unsigned short i = 0; i < mesh->getNumSubMeshes(); ++i)
//	{
//		Ogre::SubMesh* submesh = mesh->getSubMesh( i );
//
//        // We only need to add the shared vertices once
//		if(submesh->useSharedVertices)
//		{
//			if( !added_shared )
//			{
//				vertex_count += mesh->sharedVertexData->vertexCount;
//				added_shared = true;
//			}
//		}
//		else
//		{
//			vertex_count += submesh->vertexData->vertexCount;
//		}
//
//        // Add the indices
//		index_count += submesh->indexData->indexCount;
//	}
//
//
//    // Allocate space for the vertices and indices
//	estimateVertexCount(vertex_count);
//	estimateIndexCount(index_count);
//
//	added_shared = false;
//
//    // Run through the submeshes again, adding the data into the arrays
//	for ( unsigned short i = 0; i < mesh->getNumSubMeshes(); ++i)
//	{
//		Ogre::SubMesh* submesh = mesh->getSubMesh(i);
//
//        //----------------------------------------------------------------
//        // GET VERTEXDATA
//        //----------------------------------------------------------------
//		Ogre::VertexData* vertex_data;
//
//        //When there is animation:
//		if(useSoftwareBlendingVertices)
//			vertex_data = submesh->useSharedVertices ? entity->_getSkelAnimVertexData() : entity->getSubEntity(i)->_getSkelAnimVertexData();
//		else
//			vertex_data = submesh->useSharedVertices ? mesh->sharedVertexData : submesh->vertexData;
//
//
//		if((!submesh->useSharedVertices)||(submesh->useSharedVertices && !added_shared))
//		{
//			if(submesh->useSharedVertices)
//			{
//				added_shared = true;
//				shared_offset = current_offset;
//			}
//
//			const Ogre::VertexElement* posElem =
//				vertex_data->vertexDeclaration->findElementBySemantic(Ogre::VES_POSITION);
//
//			Ogre::HardwareVertexBufferSharedPtr vbuf =
//				vertex_data->vertexBufferBinding->getBuffer(posElem->getSource());
//
//			unsigned char* vertex =
//				static_cast<unsigned char*>(vbuf->lock(Ogre::HardwareBuffer::HBL_READ_ONLY));
//
//            // There is _no_ baseVertexPointerToElement() which takes an Ogre::float or a double
//            //  as second argument. So make it float, to avoid trouble when Ogre::float will
//            //  be comiled/typedefed as double:
//            //      Ogre::Real* pReal;
//			float* pReal;
//
//			for( int j = 0; j < vertex_data->vertexCount; ++j, vertex += vbuf->getVertexSize())
//			{
//				posElem->baseVertexPointerToElement(vertex, &pReal);
//
//				Ogre::Vector3 pt(pReal[0], pReal[1], pReal[2]);
//
//				vertices[current_offset + j] = (orient * (pt * scale)) + position;
//			}
//
//			vbuf->unlock();
//			next_offset += vertex_data->vertexCount;
//		}
//
//
//		Ogre::IndexData* index_data = submesh->indexData;
//		int numTris = index_data->indexCount / 3;
//		Ogre::HardwareIndexBufferSharedPtr ibuf = index_data->indexBuffer;
//
//		bool use32bitindexes = (ibuf->getType() == Ogre::HardwareIndexBuffer::IT_32BIT);
//
//		void* hwBuf = ibuf->lock(Ogre::HardwareBuffer::HBL_READ_ONLY);
//
//		int offset = (submesh->useSharedVertices)? shared_offset : current_offset;
//		int index_start = index_data->indexStart;
//		int last_index = numTris*3 + index_start;
//
//		if (use32bitindexes) {
//			Ogre::uint32* hwBuf32 = static_cast<Ogre::uint32*>(hwBuf);
//			for (int k = index_start; k < last_index; ++k)
//			{
//				indices[index_offset++] = hwBuf32[k] + static_cast<Ogre::uint32>( offset );
//			}
//		} else {
//			Ogre::uint16* hwBuf16 = static_cast<Ogre::uint16*>(hwBuf);
//			for (int k = index_start; k < last_index; ++k)
//			{
//				indices[ index_offset++ ] = static_cast<Ogre::uint32>( hwBuf16[k] ) +
//					static_cast<Ogre::uint32>( offset );
//			}
//		}
//
//		ibuf->unlock();
//		current_offset = next_offset;
//	}
//	}
}
